using System;
using System.Drawing;
using System.Linq;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.Scenarios.Geometric
{
    public class RandomEllipseScenario : RandomRectangleScenario
    {
        #region | Properties |

        /// <summary>
        /// See <see cref="BasePathScenario.UseCache"/> for more details.
        /// </summary>
        protected override bool UseCache
        {
            get { return ObstacleDetectionMethod == BlockMethodType.Precise; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomEllipseScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RandomEllipseScenario(int width, int height) : base(width, height) { }

        #endregion

        #region | Cache helper methods |

        protected void AddEllipseToCache(Rectangle ellipse)
        {
            int radiusX = ellipse.Width >> 1;
            int radiusY = ellipse.Height >> 1;

            int centerX = ellipse.Left + radiusX;
            int centerY = ellipse.Top + radiusY;

            foreach (Point point in EllipseRasterizer.Enumerate(centerX, centerY, radiusX - 1, radiusY - 1, AreHollowAreasMinimized).ToList())
            {
                SetCacheBit(point.X, point.Y);
            }
        }

        #endregion

        #region << RandomGeometryScenario >>

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnReconstructGeometry"/> for more details.
        /// </summary>
        protected override void OnReconstructGeometry(Rectangle ellipse)
        {
            if (ObstacleDetectionMethod == BlockMethodType.Precise)
            {
                AddEllipseToCache(ellipse);
            }
        }

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnDrawGeometry"/> for more details.
        /// </summary>
        protected override void OnDrawGeometry(Graphics graphics, Rectangle ellipse)
        {
            if (AreHollowAreasMinimized)
            {
                graphics.FillEllipse(Brushes.Black, ellipse);
            }
            else
            {
                ellipse = new Rectangle(ellipse.X, ellipse.Y, ellipse.Width - 1, ellipse.Height - 1);
                graphics.DrawEllipse(Pens.Black, ellipse);
            }
        }

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnIsGeometryBlocking"/> for more details.
        /// </summary>
        protected override bool OnIsGeometryBlocking(int x, int y, Rectangle ellipse)
        {
            return ObstacleDetectionMethod == BlockMethodType.Precise ? GetCacheBit(x, y) : base.OnCanGeometryBlock(x, y, ellipse);
        }

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnCanGeometryBlock"/> for more details.
        /// </summary>
        protected override bool OnCanGeometryBlock(int x, int y, Rectangle ellipse)
        {
            return ellipse.Contains(x, y);
        }

        #endregion
    }
}
