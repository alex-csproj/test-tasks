namespace TestTasks.Robots.Contracts
{
    public interface IStateParser
    {
        (Position Position, Orientation Orientation) Parse(string state);
    }
}