namespace TestTasks.Robots.Contract
{
    public interface IStateParser
    {
        (Position Position, Orientation Orientation) Parse(string state);
    }
}