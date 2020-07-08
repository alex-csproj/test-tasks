using System.Collections.Generic;

namespace TestTasks.Robots.Contracts
{
    public interface IArea
    {
        bool Contains(Position position);

        List<Position> ScentPosition { get; }
    }
}