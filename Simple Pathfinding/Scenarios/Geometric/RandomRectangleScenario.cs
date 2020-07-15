using System;
using System.Collections.Generic;
using System.Drawing;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.Scenarios.Geometric
{
    public class RandomRectangleScenario : BaseGeometryScenario<Rectangle>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomRectangleScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RandomRectangleScenario(int width, int height) : base(width, height) { }

        #endregion

        #region | Cache helper methods |

        protected void AddRectangleToCache(Rectangle rectangle)
        {
            // set bits to each point inside the rectangle
            if (AreHollowAreasMinimized)
            {
                int offset = GetOffset(rectangle.Left, rectangle.Top);

                for (int y = 0; y < rectangle.Height; y++)
                {
                    for (int x = 0; x < rectangle.Width; x++)
                    {
                        Cache.Set(offset + x, true);
                    }

                    offset += Width;
                }
            }
            else // set bits of hollow rectangle only
            {
                // sides (top/right/bottom/left)
                foreach (Point point in LineRasterizer.EnumerateHorizontalLine(rectangle.Left, rectangle.Right - 2, rectangle.Top)) SetCacheBit(point.X, point.Y);
                foreach (Point point in LineRasterizer.EnumerateVerticalLine(rectangle.Right - 1, rectangle.Top, rectangle.Bottom - 2)) SetCacheBit(point.X, point.Y);
                foreach (Point point in LineRasterizer.EnumerateHorizontalLine(rectangle.Left + 1, rectangle.Right - 1, rectangle.Bottom - 1)) SetCacheBit(point.X, point.Y);
                foreach (Point point in LineRasterizer.EnumerateVerticalLine(rectangle.Left, rectangle.Top + 1, rectangle.Bottom - 1)) SetCacheBit(point.X, point.Y);
            }
        }

        #endregion

        #region << RandomGeometryScenario >>

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnConstructLayout"/> for more details.
        /// </summary>
        protected override void OnConstructLayout(out IEnumerable<Rectangle> geometry)
        {
            List<Rectangle> result = new List<Rectangle>();

            for (int index = 0; index < 6; index++)
            {
                int x1 = Random.Next(Width - 100) + 50;
                int y1 = Random.Next(Height - 100) + 50;
                int x2 = Random.Next(Width - 100) + 50;
                int y2 = Random.Next(Height - 100) + 50;

                Rectangle rectangle = new Rectangle(Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x2 - x1), Math.Abs(y2 - y1));
                result.Add(rectangle);
            }

            geometry = result;
        }

        /// <summary>
        /// See <see cref="BasePathScenario.OnDraw"/> for more details.
        /// </summary>
        protected override void OnDrawGeometry(Graphics graphics, Rectangle rectangle)
        {
            if (AreHollowAreasMinimized)
            {
                graphics.FillRectangle(Brushes.Black, rectangle);
            }
            else
            {
                Rectangle hollow = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
                graphics.DrawRectangle(Pens.Black, hollow);
            }
        }

        protected override bool OnCanGeometryBlock(int x, int y, Rectangle geometry)
        {
            bool result;

            // empty rectangle
            if (geometry.Width == 0 && geometry.Height == 0)
            {
                result = false;
            }
            else // consider whole area
            {
                result = geometry.Contains(x, y);
            }

            return result;
        }

        protected override bool OnIsGeometryBlocking(int x, int y, Rectangle ellipse)
        {
            bool result;

            if (ellipse.Width == 1 && ellipse.Height == 1) // point
            {
                result = ellipse.X == x && ellipse.Y == y;
            }
            else if (AreHollowAreasMinimized) // filled area
            {
                return true; // already guaranteed by OnCanGeometryBlock
            }
            else // hollow area, only sides are being checked
            {
                result = (x == ellipse.Left && y >= ellipse.Top && y <= ellipse.Bottom) || // left
                         (y == ellipse.Top && x >= ellipse.Left && x <= ellipse.Right) || // top
                         (x == ellipse.Right - 1 && y >= ellipse.Top && y <= ellipse.Bottom) || // right
                         (y == ellipse.Bottom - 1 && x >= ellipse.Left && x <= ellipse.Right);  // bottom
            }

            return result;
        }

        #endregion
    }
}
