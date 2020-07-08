namespace TestTasks.Robots.Contracts
{
    public interface IRobot
    {
        Orientation Orientation { get; set; }

        Position LastPosition { get; set; }

        Status Status { get; }
    }
}