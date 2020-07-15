using System;
using System.Drawing;

namespace SimplePathfinding.PathFinders.Dijkstra
{
    public class DijkstraMap : BaseDijkstraMap<DijkstraNode>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="DijkstraMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DijkstraMap(int width, int height) : base(width, height) { }

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
        protected override DijkstraNode OnCreateNode(Point point, DijkstraNode origin, params object[] arguments)
        {
            int score = arguments != null && arguments.Length > 0 ? (int)arguments[0] : 0;
            return new DijkstraNode(point, origin, score);
        }

        #endregion
    }
}
