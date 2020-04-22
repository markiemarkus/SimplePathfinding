using System.Drawing;

namespace YinYang.CodeProject.Projects.SimplePathfinding.PathFinders.Common
{
    public class SimpleNode : BaseGraphSearchNode<SimpleNode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleNode"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="origin">The origin.</param>
        public SimpleNode(Point point, SimpleNode origin = null) : base(point, origin) { }
    }
}
