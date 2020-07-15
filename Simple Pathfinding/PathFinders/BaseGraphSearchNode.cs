using System;
using System.Drawing;

namespace SimplePathfinding.PathFinders
{
    public abstract class BaseGraphSearchNode<TNode> where TNode : BaseGraphSearchNode<TNode>
    {
        #region | Properties |

        /// <summary>
        /// Gets the node coordinates in a map.
        /// </summary>
        public Point Point { get; private set; }

        /// <summary>
        /// Gets the origin (the node from which this node was opened).
        /// </summary>
        public TNode Origin { get; protected set; }

        /// <summary>
        /// Determiens whether the node was already processed (true) or not.
        /// </summary>
        public bool IsClosed { get; private set; }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="TNode" /> struct.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="origin">The origin.</param>
        protected BaseGraphSearchNode(Point point, TNode origin = null)
        {
            Point = point;
            Origin = origin;
            IsClosed = false;
        }

        #endregion

        #region | Methods |

        /// <summary>
        /// Marks node as closed.
        /// </summary>
        public void MarkClosed()
        {
            IsClosed = true;
        }

        #endregion

        #region << IEquatable >>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TNode other)
        {
            return Point == other.Point;
        }

        #endregion

        #region << Object >>

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}", Point.X, Point.Y);
        }

        #endregion
    }
}
