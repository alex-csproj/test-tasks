using System.Collections.Generic;

namespace TestTasks.Robots.Contract
{
    public interface ICommandParser
    {
        IReadOnlyCollection<IRobotCommand> Parse(string commands);
    }
}