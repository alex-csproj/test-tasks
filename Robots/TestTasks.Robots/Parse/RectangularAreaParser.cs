using TestTasks.Robots.Contracts;

namespace TestTasks.Robots.Parse
{
    public class RectangularAreaParser : IAreaParser
    {
        public IArea Parse(string area)
        {
            ParamGuard.NotNull(area, nameof(area));

            var values = area.Split();

            int x = default, y = default;

            ParamGuard.SupportedFormat(area, nameof(area), _ => values.Length == 2 &&
                                                                int.TryParse(values[0], out x) &&
                                                                int.TryParse(values[1], out y));

            return new RectangularArea(Position.Origin, new Position(x, y));
        }
    }
}