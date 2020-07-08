namespace TestTasks.Robots
{
    public class Position
    {
        public static Position Origin = new Position(0, 0);

        public int X { get; }

        public int Y { get; }

        public Position(int x, int y) =>
            (X, Y) = (x, y);

        public override string ToString() =>
            $"{{X: {X}; Y: {Y}}}";

        public override bool Equals(object obj) =>
            obj is Position position &&
            X.Equals(position.X) &&
            Y.Equals(position.Y);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = 23 * hash + X.GetHashCode();
                hash = 23 * hash + Y.GetHashCode();
                return hash;
            }
        }
    }
}