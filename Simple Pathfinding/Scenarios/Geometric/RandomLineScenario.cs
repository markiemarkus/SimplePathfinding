using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.Scenarios.Geometric
{
    public class RandomLineScenario : RandomRectangleScenario
    {
        private const float LineWidth = 5.0f;
        private const float LineHalfWidth = LineWidth/2.0f;

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLineScenario" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RandomLineScenario(int width, int height) : base(width, height) { }

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
        protected override bool OnIsGeometryBlocking(int x, int y, Rectangle line)
        {
            bool result;

            if (ObstacleDetectionMethod == BlockMethodType.Precise)
            {
                int deltaX = line.Right - line.Left;
                int deltaY = line.Bottom - line.Top;

                // determines normalized shift
                double shiftX = -deltaY/Math.Sqrt(deltaY*deltaY + deltaX*deltaX);
                double shiftY = deltaX/Math.Sqrt(deltaY*deltaY + deltaX*deltaX);

                // left side line
                int leftAx = (int) Math.Round(-LineHalfWidth*shiftX + line.Left);
                int leftAy = (int) Math.Round(-LineHalfWidth*shiftY + line.Top);
                int leftBx = (int) Math.Round(-LineHalfWidth*shiftX + line.Right);
                int leftBy = (int) Math.Round(-LineHalfWidth*shiftY + line.Bottom);

                Point leftA = new Point(leftAx, leftAy);
                Point leftB = new Point(leftBx, leftBy);

                // right side line
                int rightAx = (int) Math.Round(LineHalfWidth*shiftX + line.Left);
                int rightAy = (int) Math.Round(LineHalfWidth*shiftY + line.Top);
                int rightBx = (int) Math.Round(LineHalfWidth*shiftX + line.Right);
                int rightBy = (int) Math.Round(LineHalfWidth*shiftY + line.Bottom);

                Point rightA = new Point(rightAx, rightAy);
                Point rightB = new Point(rightBx, rightBy);

                List<Point> points = new List<Point> { leftA, leftB, rightB, rightA };

                result = false;

                for (int index = 0, last = 3; index < 4; last = index++) 
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
        protected override bool OnCanGeometryBlock(int x, int y, Rectangle line)
        {
            return line.Contains(x, y);
        }

        #endregion
    }
}
