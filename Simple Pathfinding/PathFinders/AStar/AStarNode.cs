using System;
using System.Drawing;

namespace SimplePathfinding.PathFinders.AStar
{
    public class AStarNode : BaseGraphSearchNode<AStarNode>, IComparable<AStarNode>
    {
        #region | Properties |

        /// <summary>
        /// Gets the actual score (distance to a finish).
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Gets or sets the estimated score.
        /// </summary>
        public int EstimatedScore { get; set; }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="AStarNode" /> struct.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="score">The score.</param>
        /// <param name="estimatedScore">The estimated score.</param>
        public AStarNode(Point point, AStarNode origin = null, int score = 0, int estimatedScore = 0) : base(point, origin)
        {
            Score = score;
            EstimatedScore = estimatedScore;
        }

        #endregion

        #region | Methods |

        /// <summary>
        /// Updates the parameters on the fly.
        /// </summary>
        public void Update(int score, int estimatedScore, AStarNode origin)
        {
            Score = score;
            EstimatedScore = estimatedScore;
            Origin = origin;
        }

        #endregion

        #region << IComparable >>

        /// <summary>
        /// See <see cref="IComparable{T}.CompareTo"/> for more details.
        /// </summary>
        public int CompareTo(AStarNode other)
        {
            return EstimatedScore.CompareTo(other.EstimatedScore);
        }

        #endregion

        #region << Object >>

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}, Score = {2}, Estimated score = {3}", Point.X, Point.Y, Score, EstimatedScore);
        }

        #endregion
    }
}
