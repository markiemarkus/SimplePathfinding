using System;
using System.Collections.Generic;
using System.Drawing;
using YinYang.CodeProject.Projects.SimplePathfinding.Helpers;
using YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.AStar;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.JumpPoint
{
    public class JumpPointPathfinder : AStarPathfinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JumpPointPathfinder"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public JumpPointPathfinder(Int32 width, Int32 height) : base(width, height) { }

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.OnEnumerateNeighbors"/> for more details.
        /// </summary>
        protected override IEnumerable<Point> OnEnumerateNeighbors(AStarNode currentNode, StopFunction stopFunction)
        {
            List<Point> result = new List<Point>();

            if (currentNode.Origin != null)
            {
                Int32 x = currentNode.Point.X;
                Int32 y = currentNode.Point.Y;
                Int32 px = currentNode.Origin.Point.X;
                Int32 py = currentNode.Origin.Point.Y;

                // get the normalized direction of travel
                Int32 deltaX = (x - px)/Math.Max(Math.Abs(x - px), 1);
                Int32 deltaY = (y - py)/Math.Max(Math.Abs(y - py), 1);

                // search diagonally
                if (deltaX != 0 && deltaY != 0)
                {
                    if (!stopFunction(x, y + deltaY))
                    {
                        result.Add(new Point(x, y + deltaY));
                    }

                    if (!stopFunction(x + deltaX, y))
                    {
                        result.Add(new Point(x + deltaX, y));
                    }

                    if (!stopFunction(x, y + deltaY) || !stopFunction(x + deltaX, y))
                    {
                        result.Add(new Point(x + deltaX, y + deltaY));
                    }

                    if (stopFunction(x - deltaX, y) && !stopFunction(x, y + deltaY))
                    {
                        result.Add(new Point(x - deltaX, y + deltaY));
                    }

                    if (stopFunction(x, y - deltaY) && !stopFunction(x + deltaX, y))
                    {
                        result.Add(new Point(x + deltaX, y - deltaY));
                    }
                }
                else // search horizontally/vertically
                {
                    if (deltaX == 0)
                    {
                        if (!stopFunction(x, y + deltaY))
                        {
                            result.Add(new Point(x, y + deltaY));

                            if (stopFunction(x + 1, y) && !stopFunction(x + 1, y + deltaY))
                            {
                                result.Add(new Point(x + 1, y + deltaY));
                            }

                            if (stopFunction(x - 1, y) && !stopFunction(x - 1, y + deltaY))
                            {
                                result.Add(new Point(x - 1, y + deltaY));
                            }
                        }
                    }
                    else
                    {
                        if (!stopFunction(x + deltaX, y))
                        {
                            result.Add(new Point(x + deltaX, y));

                            if (stopFunction(x, y + 1) && !stopFunction(x + deltaX, y + 1))
                            {
                                result.Add(new Point(x + deltaX, y + 1));
                            }
                            if (stopFunction(x, y - 1) && !stopFunction(x + deltaX, y - 1))
                            {
                                result.Add(new Point(x + deltaX, y - 1));
                            }
                        }
                    }
                }
            }
            else
            {
                result.AddRange(base.OnEnumerateNeighbors(currentNode, stopFunction));
            }

            return result;
        }

        /// <summary>
        /// See <see cref="BaseGraphSearchPathfinder{TNode,TMap}.OnPerformAlgorithm"/> for more details.
        /// </summary>
        protected override void OnPerformAlgorithm(AStarNode currentNode, AStarNode neighborNode, Point neighborPoint, Point endPoint, StopFunction stopFunction)
        {
            Point jumpPoint;

            if (TryJump(neighborPoint, currentNode.Point, endPoint, stopFunction, out jumpPoint))
            {
                AStarNode jumpNode = Map[jumpPoint.X, jumpPoint.Y];

                Int32 distance = HeuristicHelper.FastEuclideanDistance(currentNode.Point, jumpPoint);
                Int32 jumpScore = currentNode.Score + distance;

                if (jumpNode == null)
                {
                    Map.OpenNode(jumpPoint, currentNode, jumpScore, jumpScore + HeuristicHelper.FastEuclideanDistance(jumpPoint, endPoint));
                }
                else if (jumpScore < jumpNode.Score)
                {
                    if (jumpNode.IsClosed) return;

                    jumpNode.Update(jumpScore, jumpScore + HeuristicHelper.FastEuclideanDistance(jumpPoint, endPoint), currentNode);
                }
            }
        }

        private static Boolean TryJump(Point sourcePoint, Point targetPoint, Point endPoint, StopFunction stopFunction, out Point jumpPoint)
        {
            Int32 x = sourcePoint.X;
            Int32 y = sourcePoint.Y;

            Int32 deltaX = x - targetPoint.X;
            Int32 deltaY = y - targetPoint.Y;

            jumpPoint = sourcePoint;

            if (stopFunction(sourcePoint.X, sourcePoint.Y)) return false;
            if (sourcePoint == endPoint) return true;

            if (deltaX != 0 && deltaY != 0) 
            {
                if (!stopFunction(x - deltaX, y + deltaY) && stopFunction(x - deltaX, y) ||
                    !stopFunction(x + deltaX, y - deltaY) && stopFunction(x, y - deltaY))
                {
                    return true;
                }
            }
            else
            {
                if (deltaX != 0)
                { 
                    if ((!stopFunction(x + deltaX, y + 1) && stopFunction(x, y + 1)) ||
                         !stopFunction(x + deltaX, y - 1) && stopFunction(x, y - 1))
                    {
                        return true;
                    }
                }
                else
                {
                    if ((!stopFunction(x + 1, y + deltaY) && stopFunction(x + 1, y)) ||
                         !stopFunction(x - 1, y + deltaY) && stopFunction(x - 1, y))
                    {
                        return true;
                    }
                }
            }

            if (deltaX != 0 && deltaY != 0)
            {
                Point dummyPoint;
                Boolean leftJump = TryJump(new Point(x + deltaX, y), sourcePoint, endPoint, stopFunction, out dummyPoint);
                Boolean bottomJump = TryJump(new Point(x, y + deltaY), sourcePoint, endPoint, stopFunction, out dummyPoint);

                if (leftJump || bottomJump) 
                {
                    return true;
                }
            }

            if (!stopFunction(x + deltaX, y) || !stopFunction(x, y + deltaY))
            {
                return TryJump(new Point(x + deltaX, y + deltaY), new Point(x, y), endPoint, stopFunction, out jumpPoint);
            }

            return false;
        }
    }
}
