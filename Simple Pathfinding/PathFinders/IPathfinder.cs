using System;
using System.Collections.Generic;
using System.Drawing;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.PathFinders
{
    public interface IPathfinder
    {
        /// <summary>
        /// Performs the search for a path from <see cref="startPoint" /> to <see cref="endPoint" />.
        /// Returning status whether the <see cref="endPoint" /> is accessible or not, and it is,
        /// also returns the list of the points leading to a <see cref="endPoint" />.
        /// </summary>
        /// <param name="startPoint">The starting point.</param>
        /// <param name="endPoint">The end position to be reached by a (to be found) path.</param>
        /// <param name="stopFunction">The stop function.</param>
        /// <param name="path">Returns the list of all the points of found path.</param>
        /// <param name="pivotPoints">Returns the list of the pivot points (sector points and corners).</param>
        /// <param name="optimize">Determines whether the optimization is turned on.</param>
        /// <returns>
        /// If set to <c>True</c> the path was found, otherwise the target point is inaccessible.
        /// </returns>
        bool TryFindPath(Point startPoint, Point endPoint,
                            StopFunction stopFunction, 
                            out IReadOnlyCollection<Point> path, 
                            out IReadOnlyCollection<Point> pivotPoints,
                            bool optimize = true);
    }
}
