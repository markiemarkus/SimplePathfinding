using System;
using System.Drawing;
using SimplePathfinding.Helpers;
using SimplePathfinding.PathFinders.Common;

namespace SimplePathfinding.PathFinders.BreadthFirst
{
    public class BreadthFirstPathfinder : BaseGraphSearchPathfinder<SimpleNode, BreadthFirstMap>
    {
        #region | Constructors |

        public BreadthFirstPathfinder(int width, int height) : base(width, height) { }

        #endregion

        #region << DijkstraPathfinder >>

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.OnPerformAlgorithm"/> for more details.
        /// </summary>
        protected override void OnPerformAlgorithm(SimpleNode currentNode, SimpleNode neighborNode, Point neighborPoint, Point endPoint, StopFunction stopFunction)
        {
            if (neighborNode == null)
            {
                Map.OpenNode(neighborPoint, currentNode);
            }
        }

        #endregion
    }
}
