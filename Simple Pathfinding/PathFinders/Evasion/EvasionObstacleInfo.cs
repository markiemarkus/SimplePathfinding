using System;
using System.Collections.Generic;
using System.Drawing;

namespace SimplePathfinding.PathFinders.Evasion
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
        public int LefPointCount { get; private set; }

        /// <summary>
        /// Gets count of the steps on the left side (start point -> other side point).
        /// </summary>
        public int LeftStepCount { get; private set; }

        /// <summary>
        /// Gets the index of the segment, that was found on the other side.
        /// </summary>
        public int SegmentIndex { get; private set; }

        /// <summary>
        /// Gets the total step count needed to go around the whole obstacle.
        /// </summary>
        public int TotalStepCount { get; private set; }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="EvasionObstacleInfo"/> struct.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="stepCount">The step count.</param>
        /// <param name="segmentIndex">Index of the segment.</param>
        public EvasionObstacleInfo(List<Point> points, int stepCount, int segmentIndex) : this()
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
        public void SetTotalStepCount(int totalStepCount)
        {
            TotalStepCount = totalStepCount;
        }

        #endregion
    }
}
