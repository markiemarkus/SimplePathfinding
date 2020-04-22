using System;
using System.Collections.Generic;
using System.Drawing;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Common;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.DepthFirst
{
    public class DepthFirstMap : BaseGraphSearchMap<SimpleNode>
    {
        private readonly Stack<SimpleNode> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthFirstMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DepthFirstMap(Int32 width, Int32 height) : base(width, height)
        {
            queue = new Stack<SimpleNode>();
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnCreateFirstNode"/> for more details.
        /// </summary>
        protected override SimpleNode OnCreateFirstNode(Point startPoint, Point endPoint)
        {
            return new SimpleNode(startPoint);
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnCreateNode"/> for more details.
        /// </summary>
        protected override SimpleNode OnCreateNode(Point point, SimpleNode origin, params object[] arguments)
        {
            return new SimpleNode(point, origin);
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnGetCount"/> for more details.
        /// </summary>
        protected override Int32 OnGetCount()
        {
            return queue.Count;
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnAddNewNode"/> for more details.
        /// </summary>
        protected override void OnAddNewNode(SimpleNode result)
        {
            queue.Push(result);
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnGetTopNode"/> for more details.
        /// </summary>
        protected override SimpleNode OnGetTopNode()
        {
            return queue.Pop();
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnClear"/> for more details.
        /// </summary>
        protected override void OnClear()
        {
            queue.Clear();
        }
    }
}
