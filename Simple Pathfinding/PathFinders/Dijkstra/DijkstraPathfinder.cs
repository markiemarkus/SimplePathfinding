using System;
using System.Drawing;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Dijkstra
{
    public class DijkstraPathfinder : BaseGraphSearchPathfinder<DijkstraNode, DijkstraMap>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="DijkstraPathfinder"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DijkstraPathfinder(Int32 width, Int32 height) : base(width, height) { }

        #endregion

        #region << BaseDijkstraFamilyPathfinder >>

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.OnPerformAlgorithm"/> for more details.
        /// </summary>
        protected override void OnPerformAlgorithm(DijkstraNode currentNode, DijkstraNode neighborNode, Point neighborPoint, Point endPoint, StopFunction stopFunction)
        {
            Int32 neighborScore = currentNode.Score + NeighborDistance(currentNode.Point, neighborPoint);

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
