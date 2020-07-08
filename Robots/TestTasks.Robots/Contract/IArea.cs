using System.Collections.Generic;

namespace TestTasks.Robots.Contract
{
    public interface IArea
    {
        bool Contains(Position position);

        List<Position> ScentPosition { get; }
    }
}