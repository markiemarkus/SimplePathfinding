using System;
using System.Drawing;
using SimplePathfinding.PathFinders.AStar;

namespace SimplePathfinding.PathFinders
{
    public abstract class BaseGraphSearchMap<TNode> where TNode : BaseGraphSearchNode<TNode>
    {
        #region | Fields |

        private readonly int width;
        private readonly int height;

        private TNode[] nodes;
        private int[] fastY;

        #endregion

        #region | Properties |

        /// <summary>
        /// Gets the open nodes count.
        /// </summary>
        public int OpenCount
        {
            get { return OnGetCount(); }
        }

        #endregion

        #region | Indexers |

        /// <summary>
        /// Gets the <see cref="AStarNode"/> on a given coordinates.
        /// </summary>
        public TNode this[int x, int y]
        {
            get { return nodes[x + fastY[y]]; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGraphSearchMap{TNode}"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BaseGraphSearchMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            Precalculate();
        }

        #endregion

        #region | Helper methods |

        private void Precalculate()
        {
            fastY = new int[height];

            for (int y = 0; y < height; y++)
            {
                fastY[y] = y * width;
            }
        }

        private void OpenNodeInternal(Point point, TNode result)
        {
            nodes[point.X + fastY[point.Y]] = result;
            OnAddNewNode(result);
        }

        #endregion

        #region | Virtual/abstract methods |

        protected abstract TNode OnCreateFirstNode(Point startPoint, Point endPoint);
        protected abstract TNode OnCreateNode(Point point, TNode origin, params object[] arguments);

        protected abstract int OnGetCount();
        protected abstract void OnAddNewNode(TNode result);
        protected abstract TNode OnGetTopNode();
        protected abstract void OnClear();

        #endregion

        #region | Methods |

        /// <summary>
        /// Creates new open node on a map at given coordinates and parameters.
        /// </summary>
        public void OpenNode(Point point, TNode origin, params object[] arguments)
        {
            TNode result = OnCreateNode(point, origin, arguments);
            OpenNodeInternal(point, result);
        }

        public void OpenFirstNode(Point startPoint, Point endPoint)
        {
            TNode result = OnCreateFirstNode(startPoint, endPoint);
            OpenNodeInternal(startPoint, result);
        }

        /// <summary>
        /// Creates the empty node at given point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public TNode CreateEmptyNode(Point point)
        {
            return OnCreateNode(point, null); 
        }

        /// <summary>
        /// Returns top node (best estimated score), and closes it.
        /// </summary>
        /// <returns></returns>
        public TNode CloseTopNode()
        {
            TNode result = OnGetTopNode();
            result.MarkClosed();
            return result;
        }

        /// <summary>
        /// Clears map for another round.
        /// </summary>
        public void Clear()
        {
            nodes = new TNode[width*height];
            OnClear();
        }

        #endregion
    }
}
