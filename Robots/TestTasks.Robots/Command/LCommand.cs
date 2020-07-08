using TestTasks.Robots.Contract;

namespace TestTasks.Robots.Command
{
    public class LCommand : IRobotCommand
    {
        private readonly IRobot robot;

        public LCommand(IRobot robot)
        {
            ParamGuard.NotNull(robot, nameof(robot));

            this.robot = robot;
        }

        public void Execute()
        {
            robot.Orientation = robot.Orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
                _ => throw ParamGuard.NotSupposedToGetHere
            };
        }
    }
}