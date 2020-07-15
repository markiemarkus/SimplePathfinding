using System;
using System.Drawing;
using SimplePathfinding.Helpers;
using SimplePathfinding.PathFinders.Dijkstra;

namespace SimplePathfinding.PathFinders.AStar
{
    public class AStarMap : BaseDijkstraMap<AStarNode>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="AStarMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public AStarMap(int width, int height) : base(width, height) { }

        #endregion

        #region << BaseDijkstraMap >>

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnCreateFirstNode"/> for more details.
        /// </summary>
        protected override AStarNode OnCreateFirstNode(Point startPoint, Point endPoint)
        {
            return new AStarNode(startPoint, null, 0, HeuristicHelper.FastEuclideanDistance(startPoint, endPoint));
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnCreateNode"/> for more details.
        /// </summary>
        protected override AStarNode OnCreateNode(Point point, AStarNode origin, params object[] arguments)
        {
            int score = arguments != null && arguments.Length > 0 ? (int)arguments[0] : 0;
            int estimatedScore = arguments != null && arguments.Length > 1 ? (int) arguments[1] : 0;
            return new AStarNode(point, origin, score, estimatedScore);
        }

        #endregion
    }
}
