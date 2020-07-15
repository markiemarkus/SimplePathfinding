using System;

namespace SimplePathfinding.Scenarios
{
    public interface IPathScenario
    {
        /// <summary>
        /// Determines whether the specified point is blocked (true), or not.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="diameter">The diameter of object to be checked.</param>
        /// <returns></returns>
        bool IsBlocked(int x, int y, int diameter);
    }
}
