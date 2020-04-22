using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios.Geometric
{
    public class RandomLineScenario : RandomRectangleScenario
    {
        private const Single LineWidth = 5.0f;
        private const Single LineHalfWidth = LineWidth/2.0f;

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLineScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RandomLineScenario(Int32 width, Int32 height) : base(width, height) { }

        #endregion

        #region << RandomGeometryScenario >>

        /// <summary>
        /// See <see cref="BasePathScenario.OnDraw"/> for more details.
        /// </summary>
        protected override void OnDrawGeometry(Graphics graphics, Rectangle line)
        {
            Pen thickPen = new Pen(Color.Black) { Width = LineWidth, StartCap = LineCap.Flat, EndCap = LineCap.Flat };
            graphics.DrawLine(thickPen, line.Left, line.Top, line.Right, line.Bottom);
        }

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnIsGeometryBlocking"/> for more details.
        /// </summary>
        protected override Boolean OnIsGeometryBlocking(Int32 x, Int32 y, Rectangle line)
        {
            Boolean result;

            if (ObstacleDetectionMethod == BlockMethodType.Precise)
            {
                Int32 deltaX = line.Right - line.Left;
                Int32 deltaY = line.Bottom - line.Top;

                // determines normalized shift
                Double shiftX = -deltaY/Math.Sqrt(deltaY*deltaY + deltaX*deltaX);
                Double shiftY = deltaX/Math.Sqrt(deltaY*deltaY + deltaX*deltaX);

                // left side line
                Int32 leftAx = (Int32) Math.Round(-LineHalfWidth*shiftX + line.Left);
                Int32 leftAy = (Int32) Math.Round(-LineHalfWidth*shiftY + line.Top);
                Int32 leftBx = (Int32) Math.Round(-LineHalfWidth*shiftX + line.Right);
                Int32 leftBy = (Int32) Math.Round(-LineHalfWidth*shiftY + line.Bottom);

                Point leftA = new Point(leftAx, leftAy);
                Point leftB = new Point(leftBx, leftBy);

                // right side line
                Int32 rightAx = (Int32) Math.Round(LineHalfWidth*shiftX + line.Left);
                Int32 rightAy = (Int32) Math.Round(LineHalfWidth*shiftY + line.Top);
                Int32 rightBx = (Int32) Math.Round(LineHalfWidth*shiftX + line.Right);
                Int32 rightBy = (Int32) Math.Round(LineHalfWidth*shiftY + line.Bottom);

                Point rightA = new Point(rightAx, rightAy);
                Point rightB = new Point(rightBx, rightBy);

                List<Point> points = new List<Point> { leftA, leftB, rightB, rightA };

                result = false;

                for (Int32 index = 0, last = 3; index < 4; last = index++) 
                {
                    if (((points[index].Y > y) != (points[last].Y > y)) && (x < (points[last].X - points[index].X)*(y - points[index].Y)/(points[last].Y - points[index].Y) + points[index].X))
                    {
                        result = !result;
                    }
                }
            }
            else
            {
                result = base.OnCanGeometryBlock(x, y, line);
            }

            return result;
        }

        /// <summary>
        /// See <see cref="BaseGeometryScenario{TGeometry}.OnCanGeometryBlock"/> for more details.
        /// </summary>
        protected override Boolean OnCanGeometryBlock(Int32 x, Int32 y, Rectangle line)
        {
            return line.Contains(x, y);
        }

        #endregion
    }
}
