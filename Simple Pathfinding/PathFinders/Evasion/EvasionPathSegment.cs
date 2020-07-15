using System.Drawing;
using SimplePathfinding.Helpers;

namespace SimplePathfinding.PathFinders.Evasion
{
    public struct PathSegment
    {
        public Point CollisionPoint { get; private set; }

        /// <summary>
        /// Gets the first point.
        /// </summary>
        public Point FirstPoint { get; private set; }

        /// <summary>
        /// Gets the last point.
        /// </summary>
        public Point LastPoint { get; private set; }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        public DirectionType Direction
        {
            get { return DirectionHelper.InfereDirection(LastPoint, CollisionPoint); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSegment" /> struct.
        /// </summary>
        /// <param name="firstPoint">The first point.</param>
        /// <param name="lastPoint">The last point.</param>
        /// <param name="collisionPoint">The collision point.</param>
        public PathSegment(Point firstPoint, Point lastPoint, Point collisionPoint) : this()
        {
            FirstPoint = firstPoint;
            LastPoint = lastPoint;
            CollisionPoint = collisionPoint;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}] - [{2}, {3}] ({4}, {5})", FirstPoint.X, FirstPoint.Y, LastPoint.X, LastPoint.Y, CollisionPoint.X, CollisionPoint.Y);
        }
    }
}
