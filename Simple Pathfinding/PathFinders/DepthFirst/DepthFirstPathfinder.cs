using System;
using System.Drawing;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Common;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.DepthFirst
{
    public class DepthFirstPathfinder : BaseGraphSearchPathfinder<SimpleNode, DepthFirstMap>
    {
        #region | Constructors |

        public DepthFirstPathfinder(Int32 width, Int32 height) : base(width, height) { }

        #endregion

        #region << DijkstraPathfinder >>

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.AllowDiagonal"/> for more details.
        /// </summary>
        public override Boolean AllowDiagonal
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
