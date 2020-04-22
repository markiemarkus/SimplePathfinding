using System;

namespace YinYang.CodeProject.Projects.SimplePathfinding.Scenarios
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
        Boolean IsBlocked(Int32 x, Int32 y, Int32 diameter);
    }
}
