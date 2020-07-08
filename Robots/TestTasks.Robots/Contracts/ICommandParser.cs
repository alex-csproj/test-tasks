using System.Collections.Generic;

namespace TestTasks.Robots.Contracts
{
    public interface ICommandParser
    {
        IEnumerable<IRobotCommand> Parse(string commands);
    }
}