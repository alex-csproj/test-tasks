using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.Robots.Command;
using TestTasks.Robots.Contracts;

namespace TestTasks.Robots.Parse
{
    public class SimpleCommandParser : ICommandParser
    {
        private static readonly string availableCommands = "LRF";
        private static readonly Predicate<string> checkSupported =
            str => str.Distinct().All(command => availableCommands.Contains(command));

        private readonly IRobot robot;

        public SimpleCommandParser(IRobot robot)
        {
            ParamGuard.NotNull(robot, nameof(robot));

            this.robot = robot;
        }

        public IReadOnlyCollection<IRobotCommand> Parse(string commands)
        {
            ParamGuard.NotNull(commands, nameof(commands));
            ParamGuard.SupportedFormat(commands, nameof(commands), checkSupported);

            return ParseImpl(commands).ToArray();
        }

        private IEnumerable<IRobotCommand> ParseImpl(string commands)
        {
            foreach (var command in commands)
                yield return command switch
                {
                    'F' => new FCommand(robot),
                    'L' => new LCommand(robot),
                    'R' => new RCommand(robot),
                    _ => throw ParamGuard.NotSupposedToGetHere
                };
        }
    }
}