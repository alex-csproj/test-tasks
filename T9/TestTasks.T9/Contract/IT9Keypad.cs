namespace TestTasks.T9.Contract
{
    public interface IT9Keypad
    {
        IT9Button GetButton(char symbol);

        bool TryGetButton(char symbol, out IT9Button button);
    }
}