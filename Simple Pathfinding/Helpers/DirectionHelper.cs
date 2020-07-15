using System;
using System.Collections.Generic;
using System.Drawing;

namespace SimplePathfinding.Helpers
{
    public class DirectionHelper
    {
        public static Point GetShift(DirectionType direction, Size size)
        {
            Point result = new Point(0, 0);

            switch (direction)
            {
                case DirectionType.North: result = new Point(size.Width >> 1, 0); break;
                case DirectionType.NorthEast: result = new Point(size.Width, 0); break;
                case DirectionType.East: result = new Point(size.Width, size.Height >> 1); break;
                case DirectionType.SouthEast: result = new Point(size.Width, size.Height); break;
                case DirectionType.South: result = new Point(size.Width >> 1, size.Height); break;
                case DirectionType.SouthWest: result = new Point(0, size.Height); break;
                case DirectionType.West: result = new Point(0, size.Height >> 1); break;
                case DirectionType.NorthWest: result = new Point(0, 0); break;
            }

            return result;
        }

        public static Point GetNextStep(Point startPoint, DirectionType direction, int steps = 1)
        {
            Point nextPoint = GetNextStep(direction, steps);
            return new Point(startPoint.X + nextPoint.X, startPoint.Y + nextPoint.Y);
        }

        public static Point GetNextStep(DirectionType direction, int steps = 1)
        {
            int x = 0, y = 0;

            if (direction.HasFlag(DirectionType.West)) x = -steps;
            if (direction.HasFlag(DirectionType.East)) x = +steps;
            if (direction.HasFlag(DirectionType.North)) y = -steps;
            if (direction.HasFlag(DirectionType.South)) y = +steps;

            return new Point(x, y);
        }

        public static DirectionType Rotate(DirectionType direction, bool leftSide, bool allowDiagonals)
        {
            return leftSide ? RotateLeft(direction, allowDiagonals) : RotateRight(direction, allowDiagonals);
        }

        public static DirectionType RotateLeft(DirectionType direction, bool allowDiagonals = true)
        {
            DirectionType result = DirectionType.None;

            if (allowDiagonals)
            {
                switch (direction)
                {
                    case DirectionType.North: result = DirectionType.NorthWest; break;
                    case DirectionType.NorthEast: result = DirectionType.North; break;
                    case DirectionType.East: result = DirectionType.NorthEast; break;
                    case DirectionType.SouthEast: result = DirectionType.East; break;
                    case DirectionType.South: result = DirectionType.SouthEast; break;
                    case DirectionType.SouthWest: result = DirectionType.South; break;
                    case DirectionType.West: result = DirectionType.SouthWest; break;
                    case DirectionType.NorthWest: result = DirectionType.West; break;
                }
            }
            else
            {
                switch (direction)
                {
                    case DirectionType.North: result = DirectionType.West; break;
                    case DirectionType.NorthEast: result = DirectionType.North; break;
                    case DirectionType.East: result = DirectionType.North; break;
                    case DirectionType.SouthEast: result = DirectionType.East; break;
                    case DirectionType.South: result = DirectionType.East; break;
                    case DirectionType.SouthWest: result = DirectionType.South; break;
                    case DirectionType.West: result = DirectionType.South; break;
                    case DirectionType.NorthWest: result = DirectionType.West; break;
                }
            }

            return result;
        }

        public static DirectionType RotateRight(DirectionType direction, bool allowDiagonals = true)
        {
            DirectionType result = DirectionType.None;

            if (allowDiagonals)
            {
                switch (direction)
                {
                    case DirectionType.North: result = DirectionType.NorthEast; break;
                    case DirectionType.NorthEast: result = DirectionType.East; break;
                    case DirectionType.East: result = DirectionType.SouthEast; break;
                    case DirectionType.SouthEast: result = DirectionType.South; break;
                    case DirectionType.South: result = DirectionType.SouthWest; break;
                    case DirectionType.SouthWest: result = DirectionType.West; break;
                    case DirectionType.West: result = DirectionType.NorthWest; break;
                    case DirectionType.NorthWest: result = DirectionType.North; break;
                }
            }
            else
            {
                switch (direction)
                {
                    case DirectionType.North: result = DirectionType.East; break;
                    case DirectionType.NorthEast: result = DirectionType.East; break;
                    case DirectionType.East: result = DirectionType.South; break;
                    case DirectionType.SouthEast: result = DirectionType.South; break;
                    case DirectionType.South: result = DirectionType.West; break;
                    case DirectionType.SouthWest: result = DirectionType.West; break;
                    case DirectionType.West: result = DirectionType.North; break;
                    case DirectionType.NorthWest: result = DirectionType.North; break;
                }
            }

            return result;
        }

        public static DirectionType InfereDirection(Point previousPoint, Point nextPoint)
        {
            int deltaX = nextPoint.X - previousPoint.X;
            int deltaY = nextPoint.Y - previousPoint.Y;

            return InfereDirection(deltaX, deltaY);
        }

        public static DirectionType InfereDirection(int deltaX, int deltaY)
        {
            DirectionType result = DirectionType.None;

            if (deltaX < 0) result |= DirectionType.West;
            if (deltaX > 0) result |= DirectionType.East;
            if (deltaY < 0) result |= DirectionType.North;
            if (deltaY > 0) result |= DirectionType.South;

            return result;
        }

        public static DirectionType Reverse(DirectionType direction)
        {
            DirectionType result = DirectionType.None;

            switch (direction)
            {
                case DirectionType.North: result = DirectionType.South; break;
                case DirectionType.NorthEast: result = DirectionType.SouthWest; break;
                case DirectionType.East: result = DirectionType.West; break;
                case DirectionType.SouthEast: result = DirectionType.NorthWest; break;
                case DirectionType.South: result = DirectionType.North; break;
                case DirectionType.SouthWest: result = DirectionType.NorthEast; break;
                case DirectionType.West: result = DirectionType.East; break;
                case DirectionType.NorthWest: result = DirectionType.SouthEast; break;
            }

            return result;
        }

        public static IEnumerable<DirectionType> GetValues(bool allowDiagonals = true)
        {
            yield return DirectionType.North;
            if (allowDiagonals) yield return DirectionType.NorthEast;
            yield return DirectionType.East;
            if (allowDiagonals) yield return DirectionType.SouthEast;
            yield return DirectionType.South;
            if (allowDiagonals) yield return DirectionType.SouthWest;
            yield return DirectionType.West;
            if (allowDiagonals) yield return DirectionType.NorthWest;
        }
    }
}
