using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders
{
    public abstract class BasePathfinder : IPathfinder
    {
        #region | Constants |

        private const Int32 TooFewPoints = 3;
        private const Int32 TooMuchPoints = 200;
        private const Int32 OptimizationStep = 20;

        #endregion

        #region | Abstract/virtual methods |

        /// <summary>
        /// See <see cref="IPathfinder.TryFindPath"/> for more details.
        /// </summary>
        protected abstract Boolean OnTryFindPath(Point startPoint, Point endPoint,
                                                 StopFunction stopFunction,
                                                 out IReadOnlyCollection<Point> path,
                                                 out IReadOnlyCollection<Point> pivotPoints,
                                                 Boolean optimize = true);

        /// <summary>
        /// Performs path optimization. This is only a stub, that passes original path (and pivot points).
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="inputPivotPoints">The input pivot points.</param>
        /// <param name="stopFunction">The stop function.</param>
        /// <param name="optimizedPath">The optimized path.</param>
        /// <param name="optimizedPivotPoints">The optimized pivot points.</param>
        protected virtual void OnOptimizePath(IReadOnlyCollection<Point> inputPath, 
                                              IReadOnlyCollection<Point> inputPivotPoints,
                                              StopFunction stopFunction,
                                              out IReadOnlyCollection<Point> optimizedPath, 
                                              out IReadOnlyCollection<Point> optimizedPivotPoints)
        {
            optimizedPath = inputPath;
            optimizedPivotPoints = inputPivotPoints;

            // cannot optimize three points or less (2 - straight line, 3 - only one important point that cannot be optimized away)
            if (inputPath.Count > TooFewPoints)
            {
                if (inputPath.Count > TooMuchPoints)
                {
                    RecursiveDivisionOptimization(inputPath, stopFunction, out optimizedPath, out optimizedPivotPoints);
                    inputPath = optimizedPath;
                }

                VisibilityOptimization(inputPath, stopFunction, out optimizedPath, out optimizedPivotPoints);
                Int32 lastCount = 0;

                while (optimizedPath.Count != lastCount)
                {
                    lastCount = optimizedPath.Count;
                    VisibilityOptimization(optimizedPath, stopFunction, out optimizedPath, out optimizedPivotPoints);
                }
            }
        }

        #endregion

        #region | Optimization methods |

        /// <summary>
        /// This optimization should be used on limited point set (see <see cref="TooMuchPoints"/>).
        /// It searches for unblocked connection from start to end point. The end point is moved,
        /// until the connection is found (at worst the next connection = no optimizaton). If found
        /// the start point is moved to this new point, and end point is reset to end point. 
        /// This continues unless we're done.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="stopFunction">The stop function.</param>
        /// <param name="optimizedPath">The optimized path.</param>
        /// <param name="optimizedPivotPoints">The optimized pivot points.</param>
        protected static void VisibilityOptimization(IReadOnlyCollection<Point> inputPath,
                                                     StopFunction stopFunction,
                                                     out IReadOnlyCollection<Point> optimizedPath,
                                                     out IReadOnlyCollection<Point> optimizedPivotPoints)
        {
            // creates result path
            List<Point> result = new List<Point>();

            // determines master point (one tested from), and last point (to detect cycle end)
            Int32 masterIndex = 0;
            Int32 lastIndex = inputPath.Count - 1;
            Point masterPoint = inputPath.ElementAt(masterIndex);

            // adds first point
            result.Add(masterPoint);

            do // performs optimization loop
            {
                // starts at last points and work its way to the start point
                for (Int32 index = Math.Min(OptimizationStep, lastIndex); index >= 0; index--)
                {
                    Int32 referenceIndex = Math.Min(masterIndex + index, lastIndex);
                    Point referencePoint = inputPath.ElementAt(referenceIndex);

                    // if reference point is visible from master point (or next, which is assumed as visible) reference point becomes master
                    if (referenceIndex == masterIndex + 1 || LineRasterizer.IsUnblocked(masterPoint, referencePoint, stopFunction))
                    {
                        // switches reference point as master, adding master to an optimized path
                        masterPoint = inputPath.ElementAt(referenceIndex);
                        masterIndex = referenceIndex;
                        result.Add(masterPoint);
                        break;
                    }
                }
            } while (masterIndex < lastIndex); // if we're on the last point -> terminate

            // returns the optimized path
            optimizedPath = result;
            optimizedPivotPoints = result;
        }

        protected static void RecursiveDivisionOptimization(IReadOnlyCollection<Point> inputPath,
                                                            StopFunction stopFunction,
                                                            out IReadOnlyCollection<Point> optimizedPath,
                                                            out IReadOnlyCollection<Point> optimizedPivotPoints)
        {
            // creates result path
            List<Point> prunedSectors = new List<Point>();

            // perfroms subdivision optimization (start -> end)
            OptimizeSegment(0, inputPath.Count - 1, stopFunction, inputPath, prunedSectors);

            // returns the optimized path
            optimizedPath = prunedSectors;
            optimizedPivotPoints = prunedSectors;
        }

        private static void OptimizeSegment(Int32 startIndex, Int32 endIndex, 
                                            StopFunction stopFunction, 
                                            IReadOnlyCollection<Point> inputPath, 
                                            ICollection<Point> result)
        {
            Point startPoint = inputPath.ElementAt(startIndex);
            Point endPoint = inputPath.ElementAt(endIndex);

            // if this segment is unblocked, return start + end points
            if (LineRasterizer.IsUnblocked(startPoint, endPoint, stopFunction))
            {
                result.Add(inputPath.ElementAt(startIndex));
                result.Add(inputPath.ElementAt(endIndex));
            }
            else // otherwise subdivide segment in two, and process them
            {
                Int32 halfIndex = startIndex + (endIndex - startIndex) / 2 + 1;
                OptimizeSegment(startIndex, halfIndex - 1, stopFunction, inputPath, result);
                OptimizeSegment(halfIndex, endIndex, stopFunction, inputPath, result);
            }
        }

        #endregion

        #region << IPathFinder >>

        /// <summary>
        /// See <see cref="IPathfinder.TryFindPath"/> for more details.
        /// </summary>
        public Boolean TryFindPath(Point startPoint, Point endPoint,
                                   StopFunction stopFunction,
                                   out IReadOnlyCollection<Point> path, 
                                   out IReadOnlyCollection<Point> pivotPoints,
                                   Boolean optimize = true)
        {
            // creates obstacle function
            pivotPoints = null;
            path = null;

            // start or finish are blocked, we cannot find path
            if (stopFunction(startPoint.X, startPoint.Y)) return false;
            if (stopFunction(endPoint.X, endPoint.Y)) return false;

            // start and finish are the same point, return 1 step path
            if (startPoint == endPoint)
            {
                path = new[] { startPoint };
                return true;
            }

            // finds the path (alternatively also optimizes/smooths it afterwards)
            Boolean result = OnTryFindPath(startPoint, endPoint, stopFunction, out path, out pivotPoints, optimize);
            if (result && optimize) OnOptimizePath(path, pivotPoints, stopFunction, out path, out pivotPoints);
            return result;
        }

        #endregion
    }
}
