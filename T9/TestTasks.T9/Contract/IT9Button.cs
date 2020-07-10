namespace TestTasks.T9.Contract
{
    public interface IT9Button : IButton
    {
        int GetPressCount(char symbol);
    }
}