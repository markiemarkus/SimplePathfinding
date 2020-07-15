using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SimplePathfinding.Helpers
{
    public class EllipseRasterizer
    {
        private const double Step = 0.001;
        private const double HalfPi = Math.PI / 2.0;

        protected static IEnumerable<Point> CreateYield(int x, int y)
        {
            yield return new Point(x, y);
        }

        public static IEnumerable<Point> Enumerate(int centerX, int centerY, int radiusX, int radiusY, bool filled = false)
        {
            IEnumerable<Point> points = Enumerate(radiusX, radiusY, filled);
            return points.Select(point => new Point(point.X + centerX - radiusX, point.Y + centerY - radiusY));
        }

        public static IEnumerable<Point> Enumerate(int radiusX, int radiusY, bool filled = false)
        {
            // preliminary check - zero point
            if (radiusX == 0 || radiusY == 0) return Enumerable.Empty<Point>();

            // preliminary check - horizontal line
            if (radiusY == 1 || radiusY == -1) return LineRasterizer.EnumerateHorizontalLine(radiusX);

            // preliminary check - vertical line
            if (radiusX == 1 || radiusX == -1) return LineRasterizer.EnumerateVerticalLine(radiusY);

            // potencial problem - negative radius X
            if (radiusX < 0) radiusX = -radiusX;

            // potencial problem - negative radius Y
            if (radiusY < 0) radiusY = -radiusY;

            // enumerates ellipse
            return EnumerateEllipse(radiusX, radiusY, filled);
        }

        /// <summary>
        /// If you wonder why I didn't use some faster algorithm (bresenham, mid-point..) it's because it wouldn't match
        /// the outline of drawn ellipse via Graphics.DrawEllipse. So the obstacle looks elsewhere then the drawn ellipse.
        /// TODO revisit in a few years.
        /// </summary>
        private static IEnumerable<Point> EnumerateEllipse(int radiusX, int radiusY, bool filled = false)
        {
            double anomaly = HalfPi;
            Point lastPoint = Point.Empty;

            List<Point> result = new List<Point>
            {
                new Point(-radiusX, radiusY), 
                new Point(radiusX, radiusY)
            };

            if (filled) result.Add(Point.Empty);

            while (anomaly >= 0.0)
            {
                int shiftX = Convert.ToInt32(radiusX*Math.Cos(anomaly));
                int shiftY = Convert.ToInt32(radiusY*Math.Sin(anomaly));

                int x = radiusX + shiftX;
                int y = radiusY + shiftY;

                if (x != lastPoint.X || y != lastPoint.Y)
                {
                    Point topLeft = new Point(radiusX - shiftX, radiusY - shiftY);
                    Point topRight = new Point(radiusX + shiftX, radiusY - shiftY);
                    Point bottomLeft = new Point(radiusX - shiftX, radiusY + shiftY);
                    Point bottomRight = new Point(radiusX + shiftX, radiusY + shiftY);

                    result.Add(topLeft);
                    if (filled) result.AddRange(LineRasterizer.EnumerateHorizontalLine(radiusX - shiftX + 1, radiusX + shiftX - 1, radiusY - shiftY));
                    result.Add(topRight);

                    result.Add(bottomLeft);
                    if (filled) result.AddRange(LineRasterizer.EnumerateHorizontalLine(radiusX - shiftX + 1, radiusX + shiftX - 1, radiusY + shiftY));
                    result.Add(bottomRight);

                    lastPoint = bottomRight;
                }

                anomaly -= Step;
            }

            return result;
        }
    }
}
