//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  gustavo_franco@hotmail.com
//
//  Copyright (C) 2006 Franco, Gustavo 
//

//
// Edit 2013 by SmartK8: Cleaned, minimized to needed methods only.
//

using System;
using System.Collections.Generic;

namespace SimplePathfinding.Helpers
{
    public class PriorityQueue<TNode> where TNode : IComparable<TNode>
    {
        #region | Fields |

        private readonly List<TNode> nodes;

        #endregion

        #region | Properties |

        public int Count
        {
            get { return nodes.Count; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{TNode}"/> class.
        /// </summary>
        public PriorityQueue()
        {
            nodes = new List<TNode>();
        }

        #endregion

        #region | Helper methods |

        private void SwapNodes(int nodeA, int nodeB)
        {
            TNode swap = nodes[nodeA];
            nodes[nodeA] = nodes[nodeB];
            nodes[nodeB] = swap;
        }

        private int Compare(int indexA, int indexB)
        {
            return nodes[indexA].CompareTo(nodes[indexB]);
        }

        #endregion

        #region | Methods |

        public void Enqueue(TNode item)
        {
            int max = nodes.Count;
            nodes.Add(item);

            do
            {
                if (max == 0) break;

                int half = (max - 1)/2;

                if (Compare(max, half) >= 0) break;
                
                SwapNodes(max, half);
                max = half;
            } 
            while (true);
        }

        public TNode Dequeue()
        {
            int p = 0;

            TNode result = nodes[0];
            nodes[0] = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);

            do
            {
                int pn = p;
                int p1 = 2*p + 1;
                int p2 = 2*p + 2;

                if (nodes.Count > p1 && Compare(p, p1) > 0) p = p1;
                if (nodes.Count > p2 && Compare(p, p2) > 0)  p = p2;

                if (p == pn) break;

                SwapNodes(p, pn);
            } 
            while (true);

            return result;
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            nodes.Clear();
        }

        #endregion
    }
}
