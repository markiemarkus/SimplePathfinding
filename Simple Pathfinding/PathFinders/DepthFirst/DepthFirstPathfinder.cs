using System;
using System.Drawing;
using SimplePathfinding.Helpers;
using SimplePathfinding.PathFinders.Common;

namespace SimplePathfinding.PathFinders.DepthFirst
{
    public class DepthFirstPathfinder : BaseGraphSearchPathfinder<SimpleNode, DepthFirstMap>
    {
        #region | Constructors |

        public DepthFirstPathfinder(int width, int height) : base(width, height) { }

        #endregion

        #region << DijkstraPathfinder >>

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.AllowDiagonal"/> for more details.
        /// </summary>
        public override bool AllowDiagonal
        {
            get { return true; }
        }

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
