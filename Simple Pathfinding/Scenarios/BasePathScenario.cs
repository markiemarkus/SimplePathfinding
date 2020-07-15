using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.Scenarios
{
    public abstract class BasePathScenario : IPathScenario
    {
        #region | Constants |

        protected readonly int BlockColorValue = Color.Black.ToArgb();

        #endregion

        #region | Fields |

        protected int Width;
        protected int Height;

        protected BitArray Cache;
        protected ushort[] DistanceMap;
        protected List<DirectionType> Directions;
        
        protected bool IsFirstRun;
        protected BlockMethodType ObstacleDetectionMethod;
        protected bool AreHollowAreasMinimized;

        #endregion

        #region | Properties |

        /// <summary>
        /// Determines whether the cache is used.
        /// </summary>
        protected virtual bool UseCache
        {
            get { return false; }
        }

        #endregion

        #region | Indexers |

        public ushort this[int x, int y]
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
        protected BasePathScenario(int width, int height)
        {
            Width = width;
            Height = height;

            int volume = Width*Height;

            Directions = DirectionHelper.GetValues().ToList();
            Cache = UseCache ? new BitArray(volume) : null;
            DistanceMap = new ushort[volume];

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
                                         bool generateNew = true,
                                         bool minimizeHollowAreas = false,
                                         bool updateDistanceMap = true,
                                         bool showDistanceMap = true)
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

        protected int GetOffset(int x, int y)
        {
            return x + y*Width;
        }

        protected bool GetCacheBit(int x, int y)
        {
            int offset = GetOffset(x, y);
            return Cache.Get(offset);
        }

        protected void SetCacheBit(int x, int y)
        {
            int offset = GetOffset(x, y);
            Cache.Set(offset, true);
        }

        #endregion

        #region | Distance map methods |

        private static void DrawDistanceIntensity(Bitmap bitmap, int x, int y, ushort distance)
        {
            int intensity = 255 - (distance << 1) % 254;
            Color color = Color.FromArgb(255, 255, 255, intensity);
            bitmap.SetPixel(x, y, color);
        }

        private void GenerateDistanceMap(Bitmap bitmap, bool showDistanceMap)
        {
            // cannot create map for invalid image, just skip it
            if (bitmap == null) return;

            // prepares for the processing
            List<Point> nextRound = new List<Point>();

            // resets the distance map to Empty = 0, Blocked = 1
            for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
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
            ushort round = 2;

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
                            ushort value = this[neighborPoint.X, neighborPoint.Y];

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

        protected void UpdateDistanceMap(Bitmap bitmap, bool generateNew, bool updateDistanceMap, bool showDistanceMap)
        {
            if (generateNew || updateDistanceMap || IsFirstRun)
            {
                GenerateDistanceMap(bitmap, showDistanceMap);
            }
            else if (showDistanceMap) // if we're not showing the map just skip this at all, otherwise redraw it
            {
                for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                        ushort distance = this[x, y];

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
        protected virtual Bitmap OnCreateDefaultImage(bool generateNew,
                                                      bool updateDistanceMap,
                                                      bool showDistanceMap)
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
        protected virtual void OnBuild(bool generateNew)
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
        protected abstract bool OnIsBlocked(int x, int y);

        #endregion

        #region | Helper methods |

        /// <summary>
        /// Conforms start and end point to the bounds of [0,0] - [width, height]
        /// </summary>
        public static void CheckBounds(ref Point start, ref Point end, int width, int height)
        {
            int x1 = Math.Max(0, Math.Min(width - 1, start.X));
            int y1 = Math.Max(0, Math.Min(height - 1, start.Y));
            int x2 = Math.Max(0, Math.Min(width - 1, end.X));
            int y2 = Math.Max(0, Math.Min(height - 1, end.Y));

            start = new Point(x1, y1);
            end = new Point(x2, y2);
        }

        public static void ClearScreen(Graphics graphics, int width, int height)
        {
            graphics.Clear(SystemColors.Control);
        }

        #endregion

        #region << IPathScenario >>

        /// <summary>
        /// See <see cref="IPathScenario.IsBlocked"/> for more details.
        /// </summary>
        public bool IsBlocked(int x, int y, int diameter)
        {
            bool result = true;

            // tests if the point is even within the bounds
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                // retrieves distance (allowed radius) from the nearest obstacle; otherwise assume worst case scenario (1)
                ushort radius = this[x, y];

                // determines whether the object will fit
                bool radiusCondition = radius <= (diameter + 1) >> 1;
                result = radiusCondition || OnIsBlocked(x, y);
            }

            return result;
        }

        #endregion
    }
}
