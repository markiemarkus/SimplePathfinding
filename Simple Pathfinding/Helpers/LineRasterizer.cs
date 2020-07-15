using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SimplePathfinding.PathFinders.Evasion;

namespace SimplePathfinding.Helpers
{
    public class LineRasterizer
    {
        #region | Common |

        protected static int GetDirection(int value, out bool positive)
        {
            positive = value > 0;
            return positive ? 1 : -1;
        }

        protected static int GetDirection(int lowValue, int highValue)
        {
            return lowValue < highValue ? 1 : -1;
        }

        protected static IEnumerable<Point> CreateYield(int x, int y)
        {
            yield return new Point(x, y);
        }

        #endregion

        #region | Horizontal line |

        public static IEnumerable<Point> EnumerateHorizontalLine(int x1, int x2, int y)
        {
            IEnumerable<Point> points = EnumerateHorizontalLine(x2 - x1 + GetDirection(x1, x2));
            return points.Select(point => new Point(point.X + x1, point.Y + y));
        }

        public static IEnumerable<Point> EnumerateHorizontalLine(int width)
        {
            // preliminary check - zero point
            if (width == 0) return CreateYield(0, 0);

            // enumerates horizontal line
            return EnumerateHorizontalLineInternal(width);
        }

        private static IEnumerable<Point> EnumerateHorizontalLineInternal(int width)
        {
            // preliminary check - zero point
            if (width == 0) return CreateYield(0, 0);

            // possible problem - reversed order
            bool positive;
            int direction = GetDirection(width, out positive);

            // prepares buffer
            List<Point> result = new List<Point>();

            // enumerates units in a reverse direction
            for (int x = 0; (!positive && x > width) || (positive && x < width); x += direction)
            {
                Point point = new Point(x, 0);
                result.Add(point);
            }

            // returns the points
            return result;
        }

        #endregion

        #region | Vertical line |

        public static IEnumerable<Point> EnumerateVerticalLine(int x, int y1, int y2)
        {
            IEnumerable<Point> points = EnumerateVerticalLine(y2 - y1 + GetDirection(y1, y2));
            return points.Select(point => new Point(point.X + x, point.Y + y1));
        }

        public static IEnumerable<Point> EnumerateVerticalLine(int height, Func<Point, bool> stopFunction = null)
        {
            // preliminary check - zero point
            if (height == 0) return CreateYield(0, 0);

            // enumerates vertical line
            return EnumerateVerticalLineInternal(height);
        }

        private static IEnumerable<Point> EnumerateVerticalLineInternal(int height)
        {
            // possible problem - reversed order
            bool positive;
            int direction = GetDirection(height, out positive);

            // prepares buffer
            List<Point> result = new List<Point>();

            // enumerates units in a given direction
            for (int y = 0; (!positive && y > height) || (positive && y < height); y += direction)
            {
                Point point = new Point(0, y);
                result.Add(point);
            }

            // returns the points
            return result;
        }

        #endregion

        #region | Line |

        public static IEnumerable<Point> EnumerateLine(int x1, int y1, int x2, int y2, bool allowDiagonals = false)
        {
            IEnumerable<Point> points = EnumerateLine(x2 - x1, y2 - y1, allowDiagonals);
            return points.Select(point => new Point(point.X + x1, point.Y + y1));
        }

        public static IEnumerable<Point> EnumerateLine(int targetX, int targetY, bool allowDiagonals = false)
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

        private static IEnumerable<Point> EnumerateLineInternal(int targetX, int targetY, bool allowDiagonals = false)
        {
            // initializes the variables of the line
            int count, error;
            int x = 0, y = 0;
            int deltaX = Math.Abs(targetX);
            int deltaY = Math.Abs(targetY);
            int stepLeftX, stepLeftY;
            int stepRightX, stepRightY;
            int stepErrorLeft, stepErrorRight;

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

            bool stepLeftDiagonal = false;
            bool stepRightDiagonal = false;
            bool isLeftDominant = false;

            if (allowDiagonals)
            {
                stepLeftDiagonal = Math.Abs(stepLeftX + stepLeftY) != 1;
                stepRightDiagonal = Math.Abs(stepRightX + stepRightY) != 1;
                isLeftDominant = Math.Abs(stepErrorLeft) > Math.Abs(stepErrorRight);
            }

            // enumerates a line using a specific unit
            for (int a = 0; a < count; a++)
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
            bool isInside = true;

            foreach (Point point in EnumerateLine(start.X, start.Y, end.X, end.Y, true))
            {
                bool doStop = stopFunction(point.X, point.Y);

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

        public static bool IsUnblocked(Point start, Point end, StopFunction stopFunction)
        {
            return EnumerateLine(start.X, start.Y, end.X, end.Y).All(point => !stopFunction(point.X, point.Y));
        }

        #endregion
    }
}
