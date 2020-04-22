using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Evasion;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Helpers
{
    public class LineRasterizer
    {
        #region | Common |

        protected static Int32 GetDirection(Int32 value, out Boolean positive)
        {
            positive = value > 0;
            return positive ? 1 : -1;
        }

        protected static Int32 GetDirection(Int32 lowValue, Int32 highValue)
        {
            return lowValue < highValue ? 1 : -1;
        }

        protected static IEnumerable<Point> CreateYield(Int32 x, Int32 y)
        {
            yield return new Point(x, y);
        }

        #endregion

        #region | Horizontal line |

        public static IEnumerable<Point> EnumerateHorizontalLine(Int32 x1, Int32 x2, Int32 y)
        {
            IEnumerable<Point> points = EnumerateHorizontalLine(x2 - x1 + GetDirection(x1, x2));
            return points.Select(point => new Point(point.X + x1, point.Y + y));
        }

        public static IEnumerable<Point> EnumerateHorizontalLine(Int32 width)
        {
            // preliminary check - zero point
            if (width == 0) return CreateYield(0, 0);

            // enumerates horizontal line
            return EnumerateHorizontalLineInternal(width);
        }

        private static IEnumerable<Point> EnumerateHorizontalLineInternal(Int32 width)
        {
            // preliminary check - zero point
            if (width == 0) return CreateYield(0, 0);

            // possible problem - reversed order
            Boolean positive;
            Int32 direction = GetDirection(width, out positive);

            // prepares buffer
            List<Point> result = new List<Point>();

            // enumerates units in a reverse direction
            for (Int32 x = 0; (!positive && x > width) || (positive && x < width); x += direction)
            {
                Point point = new Point(x, 0);
                result.Add(point);
            }

            // returns the points
            return result;
        }

        #endregion

        #region | Vertical line |

        public static IEnumerable<Point> EnumerateVerticalLine(Int32 x, Int32 y1, Int32 y2)
        {
            IEnumerable<Point> points = EnumerateVerticalLine(y2 - y1 + GetDirection(y1, y2));
            return points.Select(point => new Point(point.X + x, point.Y + y1));
        }

        public static IEnumerable<Point> EnumerateVerticalLine(Int32 height, Func<Point, Boolean> stopFunction = null)
        {
            // preliminary check - zero point
            if (height == 0) return CreateYield(0, 0);

            // enumerates vertical line
            return EnumerateVerticalLineInternal(height);
        }

        private static IEnumerable<Point> EnumerateVerticalLineInternal(Int32 height)
        {
            // possible problem - reversed order
            Boolean positive;
            Int32 direction = GetDirection(height, out positive);

            // prepares buffer
            List<Point> result = new List<Point>();

            // enumerates units in a given direction
            for (Int32 y = 0; (!positive && y > height) || (positive && y < height); y += direction)
            {
                Point point = new Point(0, y);
                result.Add(point);
            }

            // returns the points
            return result;
        }

        #endregion

        #region | Line |

        public static IEnumerable<Point> EnumerateLine(Int32 x1, Int32 y1, Int32 x2, Int32 y2, Boolean allowDiagonals = false)
        {
            IEnumerable<Point> points = EnumerateLine(x2 - x1, y2 - y1, allowDiagonals);
            return points.Select(point => new Point(point.X + x1, point.Y + y1));
        }

        public static IEnumerable<Point> EnumerateLine(Int32 targetX, Int32 targetY, Boolean allowDiagonals = false)
        {
            // preliminary check - zero point
            if (targetX == 0 && targetY == 0) return CreateYield(0, 0);

            // preliminary check - vertical line
            if (targetX == 0) return EnumerateVerticalLine(0, 0, targetY);

            // preliminary check - horizontal line
            if (targetY == 0) return EnumerateHorizontalLine(0, targetX, 0);

            // enumerates line
            return EnumerateLineInternal(targetX, targetY, allowDiagonals);
        }

        private static IEnumerable<Point> EnumerateLineInternal(Int32 targetX, Int32 targetY, Boolean allowDiagonals = false)
        {
            // initializes the variables of the line
            Int32 count, error;
            Int32 x = 0, y = 0;
            Int32 deltaX = Math.Abs(targetX);
            Int32 deltaY = Math.Abs(targetY);
            Int32 stepLeftX, stepLeftY;
            Int32 stepRightX, stepRightY;
            Int32 stepErrorLeft, stepErrorRight;

            // gradual elevation (angle <= 45°)
            if (deltaX >= deltaY)
            {
                count = deltaX + 1;
                error = (deltaY << 1) - deltaX;
                stepErrorLeft = deltaY << 1;
                stepErrorRight = (deltaY - deltaX) << 1;
                stepLeftX = 1; stepRightX = 1;
                stepLeftY = 0; stepRightY = 1;
            }
            else // steep elevation (angle > 45°)
            {
                count = deltaY + 1;
                error = (deltaX << 1) - deltaY;
                stepErrorLeft = deltaX << 1;
                stepErrorRight = (deltaX - deltaY) << 1;
                stepLeftX = 0; stepRightX = 1;
                stepLeftY = 1; stepRightY = 1;
            }

            // possible problem - reversed horizontal alignment (← instead of →)
            if (targetX < 0)
            {
                stepLeftX = -stepLeftX;
                stepRightX = -stepRightX;
            }

            // possible problem - reversed vertical alignment (↓ instead of ↑)
            if (targetY < 0)
            {
                stepLeftY = -stepLeftY;
                stepRightY = -stepRightY;
            }

            Boolean stepLeftDiagonal = false;
            Boolean stepRightDiagonal = false;
            Boolean isLeftDominant = false;

            if (allowDiagonals)
            {
                stepLeftDiagonal = Math.Abs(stepLeftX + stepLeftY) != 1;
                stepRightDiagonal = Math.Abs(stepRightX + stepRightY) != 1;
                isLeftDominant = Math.Abs(stepErrorLeft) > Math.Abs(stepErrorRight);
            }

            // enumerates a line using a specific unit
            for (Int32 a = 0; a < count; a++)
            {
                // enqueues the new unit
                yield return new Point(x, y);

                // moves position one step near to end
                if (error < 0)
                {
                    if (allowDiagonals && stepLeftDiagonal)
                    {
                        if (isLeftDominant)
                        {
                            yield return new Point(x, y + stepLeftY);
                        }
                        else
                        {
                            yield return new Point(x + stepLeftX, y);
                        }
                    }

                    error += stepErrorLeft;
                    x += stepLeftX;
                    y += stepLeftY;
                }
                else
                {
                    if (allowDiagonals && stepRightDiagonal)
                    {
                        if (isLeftDominant)
                        {
                            yield return new Point(x, y + stepRightY);
                        }
                        else
                        {
                            yield return new Point(x + stepRightX, y);
                        }
                    }

                    error += stepErrorRight;
                    x += stepRightX;
                    y += stepRightY;
                }
            }
        }

        #endregion

        #region | Pathfinding |

        public static IReadOnlyList<PathSegment> ScanPathSegments(Point start, Point end, StopFunction stopFunction)
        {
            List<PathSegment> result = new List<PathSegment>();
            Point firstPoint = Point.Empty;
            Point previousPoint = Point.Empty;
            Boolean isInside = true;

            foreach (Point point in EnumerateLine(start.X, start.Y, end.X, end.Y, true))
            {
                Boolean doStop = stopFunction(point.X, point.Y);

                if (isInside != doStop)
                {
                    if (isInside)
                    {
                        firstPoint = point;
                    }
                    else
                    {
                        PathSegment pathSegment = new PathSegment(firstPoint, previousPoint, point);
                        result.Add(pathSegment);
                    }

                    isInside = !isInside;
                }

                previousPoint = point;
            }

            PathSegment lastPathSegment = new PathSegment(firstPoint, end, Point.Empty);
            result.Add(lastPathSegment);
            return result;
        }

        public static Boolean IsUnblocked(Point start, Point end, StopFunction stopFunction)
        {
            return EnumerateLine(start.X, start.Y, end.X, end.Y).All(point => !stopFunction(point.X, point.Y));
        }

        #endregion
    }
}
