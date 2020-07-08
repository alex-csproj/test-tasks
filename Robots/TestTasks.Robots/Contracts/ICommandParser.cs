using System.Collections.Generic;

namespace TestTasks.Robots.Contracts
{
    public interface ICommandParser
    {
        IReadOnlyCollection<IRobotCommand> Parse(string commands);
    }
}