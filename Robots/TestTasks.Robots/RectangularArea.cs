using System.Collections.Generic;
using TestTasks.Robots.Contract;

namespace TestTasks.Robots
{
    public class RectangularArea : IArea
    {
        public Position LowerLeft { get; }

        public Position UpperRight { get; }

        public List<Position> ScentPosition { get; } = new List<Position>();

        public RectangularArea(Position lowerLeft, Position upperRight)
        {
            ParamGuard.NotNull(lowerLeft, nameof(lowerLeft));
            ParamGuard.NotNull(upperRight, nameof(upperRight));

            (LowerLeft, UpperRight) = (lowerLeft, upperRight);
        }

        public bool Contains(Position position)
        {
            ParamGuard.NotNull(position, nameof(position));

            return position.X >= LowerLeft.X &&
                   position.X <= UpperRight.X &&
                   position.Y >= LowerLeft.Y &&
                   position.Y <= UpperRight.Y;
        }

        public override bool Equals(object obj) =>
            obj is RectangularArea area &&
            LowerLeft.Equals(area.LowerLeft) &&
            UpperRight.Equals(area.UpperRight);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = 23 * hash + LowerLeft.GetHashCode();
                hash = 23 * hash + UpperRight.GetHashCode();
                return hash;
            }
        }

        public override string ToString() =>
            $"{LowerLeft} - {UpperRight}";
    }
}