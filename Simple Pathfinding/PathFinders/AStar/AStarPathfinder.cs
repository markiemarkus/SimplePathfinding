using System;
using System.Drawing;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.PathFinders.AStar
{
    public class AStarPathfinder : BaseGraphSearchPathfinder<AStarNode, AStarMap>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="AStarPathfinder"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public AStarPathfinder(int width, int height) : base(width, height) { }

        #endregion

        #region << BaseDijkstraFamilyPathfinder >>

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.OnPerformAlgorithm"/> for more details.
        /// </summary>
        protected override void OnPerformAlgorithm(AStarNode currentNode, AStarNode neighborNode, Point neighborPoint, Point endPoint, StopFunction stopFunction)
        {
            int neighborScore = currentNode.Score + NeighborDistance(currentNode.Point, neighborPoint);

            // opens node at this position
            if (neighborNode == null)
            {
                Map.OpenNode(neighborPoint, currentNode, neighborScore, neighborScore + HeuristicHelper.FastEuclideanDistance(neighborPoint, endPoint));
            }
            else if (neighborScore < neighborNode.Score)
            {
                neighborNode.Update(neighborScore, neighborScore + HeuristicHelper.FastEuclideanDistance(neighborPoint, endPoint), currentNode);
            }
        }

        #endregion
    }
}
