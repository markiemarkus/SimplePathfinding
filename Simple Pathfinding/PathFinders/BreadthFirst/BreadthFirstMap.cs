using System;
using System.Collections.Generic;
using System.Drawing;
using SimplePathfinding.PathFinders.Common;

namespace SimplePathfinding.PathFinders.BreadthFirst
{
    public class BreadthFirstMap : BaseGraphSearchMap<SimpleNode>
    {
        private readonly Queue<SimpleNode> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadthFirstMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public BreadthFirstMap(int width, int height) : base(width, height)
        {
            queue = new Queue<SimpleNode>();
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
        protected override int OnGetCount()
        {
            return queue.Count;
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnAddNewNode"/> for more details.
        /// </summary>
        protected override void OnAddNewNode(SimpleNode result)
        {
            queue.Enqueue(result);
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnGetTopNode"/> for more details.
        /// </summary>
        protected override SimpleNode OnGetTopNode()
        {
            return queue.Dequeue();
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
