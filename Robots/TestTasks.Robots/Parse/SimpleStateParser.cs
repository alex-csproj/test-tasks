using System;
using TestTasks.Robots.Contracts;

namespace TestTasks.Robots.Parse
{
    public class SimpleStateParser : IStateParser
    {
        public (Position Position, Orientation Orientation) Parse(string state)
        {
            ParamGuard.NotNull(state, nameof(state));

            var values = state.Split();

            int x = default, y = default;
            Orientation orientation = default;

            ParamGuard.SupportedFormat(state, nameof(state), _ => values.Length == 3 &&
                                                                  int.TryParse(values[0], out x) &&
                                                                  int.TryParse(values[1], out y) &&
                                                                  Enum.TryParse(values[2], out orientation));

            return (new Position(x, y), orientation);
        }
    }
}