using TestTasks.Robots.Contracts;

namespace TestTasks.Robots.Command
{
    public class FCommand : IRobotCommand
    {
        private readonly IRobot robot;

        public FCommand(IRobot robot)
        {
            ParamGuard.NotNull(robot, nameof(robot));

            this.robot = robot;
        }

        public void Execute() =>
            robot.LastPosition = GetNewPosition();

        private Position GetNewPosition()
        {
            var x = robot.LastPosition.X;
            var y = robot.LastPosition.Y;

            return robot.Orientation switch
            {
                Orientation.N => new Position(x, ++y),
                Orientation.E => new Position(++x, y),
                Orientation.S => new Position(x, --y),
                Orientation.W => new Position(--x, y),
                _ => throw ParamGuard.NotSupposedToGetHere
            };
        }
    }
}