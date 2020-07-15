using System;
using System.Drawing;
using SimplePathfinding.Helpers;
using SimplePathfinding.PathFinders.Dijkstra;

namespace SimplePathfinding.PathFinders.BestFirst
{
    public class BestFirstPathfinder : DijkstraPathfinder
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="BestFirstPathfinder"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public BestFirstPathfinder(int width, int height) : base(width, height) { }

        #endregion

        #region << BaseDijkstraFamilyPathfinder >>

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.OnPerformAlgorithm"/> for more details.
        /// </summary>
        protected override void OnPerformAlgorithm(DijkstraNode currentNode, DijkstraNode neighborNode, Point neighborPoint, Point endPoint, StopFunction stopFunction)
        {
            int neighborScore = HeuristicHelper.FastEuclideanDistance(neighborPoint, endPoint);

            if (neighborNode == null)
            {
                Map.OpenNode(neighborPoint, currentNode, neighborScore);
            }
            else if (neighborScore < neighborNode.Score)
            {
                neighborNode.Update(neighborScore, currentNode);
            }
        }

        #endregion
    }
}
