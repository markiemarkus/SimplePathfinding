using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.PathFinders
{
    public abstract class BaseGraphSearchPathfinder<TNode, TMap> : BasePathfinder 
        where TNode : BaseGraphSearchNode<TNode>
        where TMap : BaseGraphSearchMap<TNode>
    {
        #region | Fields |

        protected readonly TMap Map;
        protected readonly IEnumerable<DirectionType> Directions;

        #endregion

        #region | Properties |

        /// <summary>
        /// Gets the width of the area in question.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the area in question.
        /// </summary>
        public int Height { get; private set; }

        #endregion

        #region | Calculated properties |

        /// <summary>
        /// Determines whether this algorithm supports diagonal directions or not.
        /// </summary>
        public virtual bool AllowDiagonal
        {
            get { return true; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGraphSearchPathfinder{TNode,TMap}"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BaseGraphSearchPathfinder(int width, int height)
        {
            Width = width;
            Height = height;

            Map = (TMap) Activator.CreateInstance(typeof(TMap), width, height);
            Directions = DirectionHelper.GetValues(AllowDiagonal);
        }

        #endregion

        #region | Helper methods |

        /// <summary>
        /// Determines the distance between neighbor points in an unified grid.
        /// </summary>
        /// <param name="start">The start point.</param>
        /// <param name="end">The neighbor point.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected static int NeighborDistance(Point start, Point end)
        {
            int result;
            int deltaX = end.X - start.X;
            int deltaY = end.Y - start.Y;
            int distance = (deltaX < 0 ? -deltaX : deltaX) + (deltaY < 0 ? -deltaY : deltaY);

            switch (distance)
            {
                case 0: result = 0; break;
                case 1: result = 1; break; 
                case 2: result = 8; break; // x^2 + y^2 (x = 2, y = 2)

                default:
                    throw new NotSupportedException();
            }

            return result;
        }

        /// <summary>
        /// Reconstructs the path from end node, back to the start node using originating node.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        /// <returns></returns>
        protected IReadOnlyList<Point> ReconstructPath(Point endPoint)
        {
            // starts at end point
            TNode origin;
            Point currentPoint = endPoint;

            // use linked list for faster insertion (to avoid reversing the array)
            LinkedList<Point> result = new LinkedList<Point>(new[] { endPoint } );

            do // tracks back the nodes to find the path
            {
                origin = Map[currentPoint.X, currentPoint.Y];

                if (origin != null)
                {
                    origin = origin.Origin;

                    if (origin != null)
                    {
                        result.AddFirst(origin.Point);
                        currentPoint = origin.Point;
                    }
                }
            } 
            while (origin != null);

            // converts it to a regular read-only collection
            return result.ToList();
        }

        /// <summary>
        /// Enumerates the neighbors points for a given node.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="stopFunction">The stop function.</param>
        /// <returns></returns>
        protected virtual IEnumerable<Point> OnEnumerateNeighbors(TNode currentNode, StopFunction stopFunction)
        {
            return Directions.
                // creates next step in this direction from current position
                Select(direction => DirectionHelper.GetNextStep(currentNode.Point, direction)).
                // selects only points that are within bounds of map
                Where(point => point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height);
        }

        #endregion

        #region | Virtual/abstract methods |

        protected abstract void OnPerformAlgorithm(TNode currentNode, TNode neighborNode, Point neighborPoint, Point endPoint, StopFunction stopFunction);

        #endregion

        #region << BasePathfinder >>

        /// <summary>
        /// See <see cref="BasePathfinder.OnTryFindPath"/> for more details.
        /// </summary>
        protected override bool OnTryFindPath(Point startPoint, Point endPoint,
                                                 StopFunction stopFunction,
                                                 out IReadOnlyCollection<Point> path,
                                                 out IReadOnlyCollection<Point> pivotPoints,
                                                 bool optimize = true)
        {
            // prepares main parameters
            bool result = false;
            pivotPoints = null;
            path = null;

            // clears the map
            Map.Clear();

            // creates start/finish nodes
            TNode endNode = Map.CreateEmptyNode(endPoint);

            // prepares first node
            Map.OpenFirstNode(startPoint, endPoint);

            while (Map.OpenCount > 0)
            {
                TNode currentNode = Map.CloseTopNode();

                // if current node is obstacle, skip it
                if (stopFunction(currentNode.Point.X, currentNode.Point.Y)) continue;

                // if we've detected end node, reconstruct the path back to the start
                if (currentNode.Equals(endNode))
                {
                    path = ReconstructPath(endPoint);
                    result = true;
                    break;
                }

                // processes all the neighbor points
                foreach (Point neighborPoint in OnEnumerateNeighbors(currentNode, stopFunction))
                {
                    // if this neighbor is obstacle skip it, it is not viable node
                    if (stopFunction(neighborPoint.X, neighborPoint.Y)) continue;

                    // determines the node if possible, whether it is closed, and calculates its score
                    TNode neighborNode = Map[neighborPoint.X, neighborPoint.Y];
                    bool inClosedSet = neighborNode != null && neighborNode.IsClosed;

                    // if this node was already processed, skip it
                    if (inClosedSet) continue;

                    // performs the implementation specific variant of graph search algorithm
                    OnPerformAlgorithm(currentNode, neighborNode, neighborPoint, endPoint, stopFunction);
                }
            }

            return result;
        }

        #endregion
    }
}
