using System;
using System.Drawing;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Dijkstra
{
    public class DijkstraMap : BaseDijkstraMap<DijkstraNode>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="DijkstraMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DijkstraMap(Int32 width, Int32 height) : base(width, height) { }

        #endregion

        #region << BaseDijkstraMap >>

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnCreateFirstNode"/> for more details.
        /// </summary>
        protected override DijkstraNode OnCreateFirstNode(Point startPoint, Point endPoint)
        {
            return new DijkstraNode(startPoint);
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnCreateNode"/> for more details.
        /// </summary>
        protected override DijkstraNode OnCreateNode(Point point, DijkstraNode origin, params Object[] arguments)
        {
            Int32 score = arguments != null && arguments.Length > 0 ? (Int32)arguments[0] : 0;
            return new DijkstraNode(point, origin, score);
        }

        #endregion
    }
}
