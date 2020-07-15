using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.PathFinders.Evasion
{
    public class EvasionPathfinder : BasePathfinder
    {
        #region | Helper methods |

        private static bool TryScanObstacle(
            Point startPoint,                           // starting point (one before hitting the obstacle)
            DirectionType direction,                    // direction of hitting the obstacle
            StopFunction stopFunction,                  
            IEnumerable<PathSegment> segments,          // path segments detected from start to end
            int segmentIndex,                         // starting segment in which the obstacle was hit
            out EvasionObstacleInfo obstacleInfo)       // useful informations about obstacle, and optimal path around
        {
            // initializes the result structures
            bool result = false;
            List<Point> cornerPointList = new List<Point>();
            obstacleInfo = default(EvasionObstacleInfo);

            // detects all the starting points from relevant segments (to be tested for potential evading path end)
            IList<Point> finishPoints = segments.
                Skip(segmentIndex + 1).
                Select(segment => segment.FirstPoint).
                ToList();

            // initalizes the parameters
            int oldSegmentIndex = segmentIndex;
            Point position = startPoint;
            int totalStepCount = 0;

            // expected direction in which start point should be re-entered (this check is essential)
            DirectionType entryDirection = RotateUntil(startPoint, direction, stopFunction, false, true);
            entryDirection = DirectionHelper.Reverse(entryDirection);

            // rotates until the direction is no longer in a collision course (in a given hand side)
            direction = RotateUntil(startPoint, direction, stopFunction, true, true);

            do
            {
                // retrieves next point in a actual direction
                Point nextPoint = DirectionHelper.GetNextStep(position, direction);
                totalStepCount++;

                // we've ended up in the start position, that means we can terminate this scan
                if (nextPoint == startPoint && direction == entryDirection) break;

                // detects whether this point coincides with any finish point
                int finishPointIndex = finishPoints.IndexOf(nextPoint);

                // we've ended up on ther other side, path was found, continue if we can find better finish point (in a higher segment)
                if (finishPointIndex >= 0 && finishPointIndex + oldSegmentIndex >= segmentIndex)
                {
                    obstacleInfo = new EvasionObstacleInfo(cornerPointList, totalStepCount, finishPointIndex);
                    result = true;
                }

                // rotates (starting at opposite direction) from the wall until it finds a passable spot
                DirectionType previousDirection = direction;
                direction = RotateUntil(nextPoint, DirectionHelper.Reverse(direction), stopFunction, true, true);
                if (direction != previousDirection) cornerPointList.Add(nextPoint);

                // advances to next point
                position = nextPoint;
            }
            while (true);

            // returns the points, and operation result
            if (result) obstacleInfo.SetTotalStepCount(totalStepCount);

            return result;
        }

        private static DirectionType RotateUntil(Point startPoint, DirectionType direction, StopFunction stopFunction, bool leftSide, bool untilFree)
        {
            bool condition;

            do // rotates until the conditions are fullfilled (determined by untilFree)
            {
                direction = DirectionHelper.Rotate(direction, leftSide, false);
                Point nextLeftPoint = DirectionHelper.GetNextStep(startPoint, direction);
                condition = untilFree ? !stopFunction(nextLeftPoint.X, nextLeftPoint.Y) : stopFunction(nextLeftPoint.X, nextLeftPoint.Y);
            }
            while (!condition);

            return direction;
        }

        #endregion

        #region << BasePathfinder >>

        /// <summary>
        /// See <see cref="BasePathfinder.OnTryFindPath"/> for more details.
        /// </summary>
        protected override bool OnTryFindPath(Point startPoint, Point endPoint,
                                                 StopFunction stopFunction,
                                                 out IReadOnlyCollection<Point> path, 
                                                 out IReadOnlyCollection<Point> pivotPoints,
                                                 bool optimize = true)
        {
            // prepares main parameters
            bool result = false;
            List<Point> pointList = new List<Point>();

            // detects all the path segments that are passable
            IReadOnlyList<PathSegment> segments = LineRasterizer.ScanPathSegments(startPoint, endPoint, stopFunction);
            int segmentIndex = 0;
            bool running = true;

            while (running)
            {
                // gets this segment
                PathSegment segment = segments[segmentIndex];
                Point collisionPoint = segment.LastPoint;

                // adds already found points to a final path
                pointList.Add(segment.FirstPoint);
                pointList.Add(segment.LastPoint);

                // we have arrived at destination, we're done here
                if (collisionPoint == endPoint)
                {
                    result = true;
                    break;
                }

                // cirumvents the obstacle from both sides
                EvasionObstacleInfo obstacleInfo;

                // tries to circumvent the obstacle from both sides (left/right) to determine which on is shorter
                if (TryScanObstacle(collisionPoint, segment.Direction, stopFunction, segments, segmentIndex, out obstacleInfo))
                {
                    // adds better route points to our result structures, advances to the latest segment
                    segmentIndex += obstacleInfo.SegmentIndex + 1;

                    // determines left/right side paths step counts
                    int leftPathStepCount = obstacleInfo.LeftStepCount;
                    int rightPathStepCount = obstacleInfo.TotalStepCount - leftPathStepCount;

                    // determines short path
                    IEnumerable<Point> shorterPath = leftPathStepCount < rightPathStepCount ? 
                        obstacleInfo.PivotPoints.Take(obstacleInfo.LefPointCount) : 
                        obstacleInfo.PivotPoints.Skip(obstacleInfo.LefPointCount).Reverse();

                    // adds this path to overall path
                    pointList.AddRange(shorterPath);
                }
                else // path not found
                {
                    running = false;
                }
            }

            // returns the found path
            pivotPoints = pointList;
            path = pointList;
            return result;
        }

        #endregion
    }
}
