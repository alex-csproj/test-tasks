using System.Collections.Generic;

namespace TestTasks.T9.Contract
{
    public interface IButton
    {
        char Label { get; }

        IReadOnlyList<char> Symbols { get; }
    }
}