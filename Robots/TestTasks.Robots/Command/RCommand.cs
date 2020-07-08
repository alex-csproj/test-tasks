using TestTasks.Robots.Contract;

namespace TestTasks.Robots.Command
{
    public class RCommand : IRobotCommand
    {
        private readonly IRobot robot;

        public RCommand(IRobot robot)
        {
            ParamGuard.NotNull(robot, nameof(robot));

            this.robot = robot;
        }

        public void Execute()
        {
            robot.Orientation = robot.Orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N,
                _ => throw ParamGuard.NotSupposedToGetHere
            };
        }
    }
}