using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios
{
    public abstract class BasePathScenario : IPathScenario
    {
        #region | Constants |

        protected readonly Int32 BlockColorValue = Color.Black.ToArgb();

        #endregion

        #region | Fields |

        protected Int32 Width;
        protected Int32 Height;

        protected BitArray Cache;
        protected UInt16[] DistanceMap;
        protected List<DirectionType> Directions;
        
        protected Boolean IsFirstRun;
        protected BlockMethodType ObstacleDetectionMethod;
        protected Boolean AreHollowAreasMinimized;

        #endregion

        #region | Properties |

        /// <summary>
        /// Determines whether the cache is used.
        /// </summary>
        protected virtual Boolean UseCache
        {
            get { return false; }
        }

        #endregion

        #region | Indexers |

        public UInt16 this[Int32 x, Int32 y]
        {
            get { return DistanceMap[x + y*Width]; }
            set { DistanceMap[x + y*Width] = value; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePathScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BasePathScenario(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;

            Int32 volume = Width*Height;

            Directions = DirectionHelper.GetValues().ToList();
            Cache = UseCache ? new BitArray(volume) : null;
            DistanceMap = new UInt16[volume];

            IsFirstRun = true;
            AreHollowAreasMinimized = true;
            ObstacleDetectionMethod = BlockMethodType.Precise;
        }

        #endregion

        #region | Methods |

        /// <summary>
        /// Creates the default image, that will be used as a reset image.
        /// </summary>
        /// <param name="obstacleDetectionMethod">Type of the method used to determine whether point is blocked (speed/quality).</param>
        /// <param name="generateNew">Whether to generate new variant of scenario (where applicable).</param>
        /// <param name="minimizeHollowAreas">Whether to minimize hollow areas, or not.</param>
        /// <param name="updateDistanceMap">Whether to recreate the distance map.</param>
        /// <param name="showDistanceMap">Whether the distance map should be visualized in the target image.</param>
        public Bitmap CreateDefaultImage(BlockMethodType obstacleDetectionMethod = BlockMethodType.Precise,
                                         Boolean generateNew = true, 
                                         Boolean minimizeHollowAreas = false, 
                                         Boolean updateDistanceMap = true,
                                         Boolean showDistanceMap = true)
        {
            ObstacleDetectionMethod = obstacleDetectionMethod;
            AreHollowAreasMinimized = minimizeHollowAreas;

            return OnCreateDefaultImage(generateNew, updateDistanceMap, showDistanceMap);
        }

        #endregion

        #region | Bit field methods |

        private void ResetCache()
        {
            if (Cache == null) Cache = new BitArray(Width*Height);

            Cache.SetAll(false);
        }

        protected Int32 GetOffset(Int32 x, Int32 y)
        {
            return x + y*Width;
        }

        protected Boolean GetCacheBit(Int32 x, Int32 y)
        {
            Int32 offset = GetOffset(x, y);
            return Cache.Get(offset);
        }

        protected void SetCacheBit(Int32 x, Int32 y)
        {
            Int32 offset = GetOffset(x, y);
            Cache.Set(offset, true);
        }

        #endregion

        #region | Distance map methods |

        private static void DrawDistanceIntensity(Bitmap bitmap, Int32 x, Int32 y, UInt16 distance)
        {
            Int32 intensity = 255 - (distance << 1) % 254;
            Color color = Color.FromArgb(255, 255, 255, intensity);
            bitmap.SetPixel(x, y, color);
        }

        private void GenerateDistanceMap(Bitmap bitmap, Boolean showDistanceMap)
        {
            // cannot create map for invalid image, just skip it
            if (bitmap == null) return;

            // prepares for the processing
            List<Point> nextRound = new List<Point>();

            // resets the distance map to Empty = 0, Blocked = 1
            for (Int32 y = 0; y < Height; y++)
            for (Int32 x = 0; x < Width; x++)
            {
                Point point = new Point(x, y);

                // determines whether the pixel is blocked, or on the boundaries (disables off-screen pathfinding)
                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1 || OnIsBlocked(x, y))
                {
                    this[x, y] = 1;
                    nextRound.Add(point);
                }
                else
                {
                    this[x, y] = 0;
                }
            }

            // round to does actual classification
            UInt16 round = 2;

            do // perform rounds if there is still at least one pixel to process
            {
                // switches the next round for current round
                List<Point> thisRound = nextRound;
                nextRound = new List<Point>();

                // processes all the neighbor points along the border of already classified pixels
                foreach (Point point in thisRound)
                foreach (DirectionType direction in Directions)
                {
                    Point neighborPoint = DirectionHelper.GetNextStep(point, direction);

                    // checks whether it is within valid bounds
                    if (neighborPoint.X >= 0 && neighborPoint.Y >= 0 &&
                        neighborPoint.X < Width && neighborPoint.Y < Height)
                    {
                        UInt16 value = this[neighborPoint.X, neighborPoint.Y];

                        // if this neighbor is still unclassified, do it, and add it for the next round
                        if (value == 0)
                        {
                            if (showDistanceMap)
                            {
                                DrawDistanceIntensity(bitmap, neighborPoint.X, neighborPoint.Y, round);
                            }

                            this[neighborPoint.X, neighborPoint.Y] = round;
                            nextRound.Add(neighborPoint);
                        }
                    }
                }

                round++;
            }
            while (nextRound.Count > 0);
        }

        protected void UpdateDistanceMap(Bitmap bitmap, Boolean generateNew, Boolean updateDistanceMap, Boolean showDistanceMap)
        {
            if (generateNew || updateDistanceMap || IsFirstRun)
            {
                GenerateDistanceMap(bitmap, showDistanceMap);
            }
            else if (showDistanceMap) // if we're not showing the map just skip this at all, otherwise redraw it
            {
                for (Int32 y = 0; y < Height; y++)
                for (Int32 x = 0; x < Width; x++)
                {
                    UInt16 distance = this[x, y];

                    // draw only non-blocking spaces
                    if (distance > 1)
                    {
                        DrawDistanceIntensity(bitmap, x, y, distance);
                    }
                }
            }
        }

        #endregion

        #region | Virtual/abstract methods |

        /// <summary>
        /// Called when a default image is about to be created.
        /// </summary>
        /// <returns></returns>
        protected virtual Bitmap OnCreateDefaultImage(Boolean generateNew, 
                                                      Boolean updateDistanceMap, 
                                                      Boolean showDistanceMap)
        {
            Bitmap result = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                if (UseCache) ResetCache();

                graphics.Clear(SystemColors.Control);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // generates scenario, if needed
                OnBuild(generateNew || IsFirstRun);

                // creates distance map, or at least updates it
                UpdateDistanceMap(result, generateNew, updateDistanceMap, showDistanceMap);

                // draws the main layout of the scenario
                OnDraw(graphics);
            }

            IsFirstRun = false;
            return result;
        }

        /// <summary>
        /// Called when a scenario is about to be built.
        /// </summary>
        /// <param name="generateNew">Indicates that a new configuration should be generated if possible.</param>
        protected virtual void OnBuild(Boolean generateNew)
        {
            // not implemented here (empty scenario)
        }

        /// <summary>
        /// Called when scenario is about to be drawn to an image.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="?">The ?.</param>
        protected virtual void OnDraw(Graphics graphics)
        {
            // not implemented here (empty scenario)
        }

        /// <summary>
        /// Determines whether the point is blocked or not. There are two variants: fast, or precise (self-explanatory).
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        protected abstract Boolean OnIsBlocked(Int32 x, Int32 y);

        #endregion

        #region | Helper methods |

        /// <summary>
        /// Conforms start and end point to the bounds of [0,0] - [width, height]
        /// </summary>
        public static void CheckBounds(ref Point start, ref Point end, Int32 width, Int32 height)
        {
            Int32 x1 = Math.Max(0, Math.Min(width - 1, start.X));
            Int32 y1 = Math.Max(0, Math.Min(height - 1, start.Y));
            Int32 x2 = Math.Max(0, Math.Min(width - 1, end.X));
            Int32 y2 = Math.Max(0, Math.Min(height - 1, end.Y));

            start = new Point(x1, y1);
            end = new Point(x2, y2);
        }

        public static void ClearScreen(Graphics graphics, Int32 width, Int32 height)
        {
            graphics.Clear(SystemColors.Control);
        }

        #endregion

        #region << IPathScenario >>

        /// <summary>
        /// See <see cref="IPathScenario.IsBlocked"/> for more details.
        /// </summary>
        public Boolean IsBlocked(Int32 x, Int32 y, Int32 diameter)
        {
            Boolean result = true;

            // tests if the point is even within the bounds
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                // retrieves distance (allowed radius) from the nearest obstacle; otherwise assume worst case scenario (1)
                UInt16 radius = this[x, y];
   
                // determines whether the object will fit
                Boolean radiusCondition = radius <= (diameter + 1) >> 1;
                result = radiusCondition || OnIsBlocked(x, y);
            }

            return result;
        }

        #endregion
    }
}
