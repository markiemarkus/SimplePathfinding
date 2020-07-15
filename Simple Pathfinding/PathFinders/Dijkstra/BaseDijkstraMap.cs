using System;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.PathFinders.Dijkstra
{
    public abstract class BaseDijkstraMap<TNode> : BaseGraphSearchMap<TNode> where TNode : BaseGraphSearchNode<TNode>, IComparable<TNode>
    {
        #region | Fields |

        private readonly PriorityQueue<TNode> priorityQueue;

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDijkstraMap{TNode}"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BaseDijkstraMap(int width, int height) : base(width, height)
        {
            priorityQueue = new PriorityQueue<TNode>();
        }

        #endregion

        #region << BaseGraphSearchMap >>

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnGetCount"/> for more details.
        /// </summary>
        protected override int OnGetCount()
        {
            return priorityQueue.Count;
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnAddNewNode"/> for more details.
        /// </summary>
        protected override void OnAddNewNode(TNode result)
        {
            priorityQueue.Enqueue(result);
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnGetTopNode"/> for more details.
        /// </summary>
        protected override TNode OnGetTopNode()
        {
            return priorityQueue.Dequeue();
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchMap{TNode}.OnClear"/> for more details.
        /// </summary>
        protected override void OnClear()
        {
            priorityQueue.Clear();
        }

        #endregion
    }
}
