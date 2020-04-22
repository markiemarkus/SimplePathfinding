using System;
using System.Drawing;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Dijkstra;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.AStar
{
    public class AStarMap : BaseDijkstraMap<AStarNode>
    {
        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="AStarMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public AStarMap(Int32 width, Int32 height) : base(width, height) { }

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
        protected override AStarNode OnCreateNode(Point point, AStarNode origin, params Object[] arguments)
        {
            Int32 score = arguments != null && arguments.Length > 0 ? (Int32)arguments[0] : 0;
            Int32 estimatedScore = arguments != null && arguments.Length > 1 ? (Int32) arguments[1] : 0;
            return new AStarNode(point, origin, score, estimatedScore);
        }

        #endregion
    }
}
