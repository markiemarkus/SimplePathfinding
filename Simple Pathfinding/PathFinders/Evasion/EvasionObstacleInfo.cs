using System;
using System.Collections.Generic;
using System.Drawing;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Evasion
{
    public struct EvasionObstacleInfo
    {
        #region | Properties |

        /// <summary>
        /// Gets the list of points (only important points like corners).
        /// </summary>
        public List<Point> PivotPoints { get; private set; }

        /// <summary>
        /// Helps to determine which point in <see cref="PivotPoints"/> is left, and from that index on it is right part.
        /// </summary>
        public Int32 LefPointCount { get; private set; }

        /// <summary>
        /// Gets count of the steps on the left side (start point -> other side point).
        /// </summary>
        public Int32 LeftStepCount { get; private set; }

        /// <summary>
        /// Gets the index of the segment, that was found on the other side.
        /// </summary>
        public Int32 SegmentIndex { get; private set; }

        /// <summary>
        /// Gets the total step count needed to go around the whole obstacle.
        /// </summary>
        public Int32 TotalStepCount { get; private set; }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="EvasionObstacleInfo"/> struct.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="stepCount">The step count.</param>
        /// <param name="segmentIndex">Index of the segment.</param>
        public EvasionObstacleInfo(List<Point> points, Int32 stepCount, Int32 segmentIndex) : this()
        {
            PivotPoints = points;
            LefPointCount = points.Count;
            LeftStepCount = stepCount;
            SegmentIndex = segmentIndex;
        }

        #endregion

        #region | Methods |

        /// <summary>
        /// Sets the total step count.
        /// </summary>
        /// <param name="totalStepCount">The total step count.</param>
        public void SetTotalStepCount(Int32 totalStepCount)
        {
            TotalStepCount = totalStepCount;
        }

        #endregion
    }
}
