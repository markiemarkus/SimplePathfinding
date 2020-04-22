using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Helpers
{
    public class EllipseRasterizer
    {
        private const Double Step = 0.001;
        private const Double HalfPi = Math.PI / 2.0;

        protected static IEnumerable<Point> CreateYield(Int32 x, Int32 y)
        {
            yield return new Point(x, y);
        }

        public static IEnumerable<Point> Enumerate(Int32 centerX, Int32 centerY, Int32 radiusX, Int32 radiusY, Boolean filled = false)
        {
            IEnumerable<Point> points = Enumerate(radiusX, radiusY, filled);
            return points.Select(point => new Point(point.X + centerX - radiusX, point.Y + centerY - radiusY));
        }

        public static IEnumerable<Point> Enumerate(Int32 radiusX, Int32 radiusY, Boolean filled = false)
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
        /// </summary>
        private static IEnumerable<Point> EnumerateEllipse(Int32 radiusX, Int32 radiusY, Boolean filled = false)
        {
            Double anomaly = HalfPi;
            Point lastPoint = Point.Empty;

            List<Point> result = new List<Point>
            {
                new Point(-radiusX, radiusY), 
                new Point(radiusX, radiusY)
            };

            if (filled) result.Add(Point.Empty);

            while (anomaly >= 0.0)
            {
                Int32 shiftX = Convert.ToInt32(radiusX*Math.Cos(anomaly));
                Int32 shiftY = Convert.ToInt32(radiusY*Math.Sin(anomaly));

                Int32 x = radiusX + shiftX;
                Int32 y = radiusY + shiftY;

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
