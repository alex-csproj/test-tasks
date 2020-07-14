using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.T9.Contract;

namespace TestTasks.T9
{
    public class SimpleT9Button : IT9Button
    {
        private readonly IButton button;
        private readonly IReadOnlyDictionary<char, int> pressCount;

        public char Label => button.Label;
        public IReadOnlyList<char> Symbols => button.Symbols;

        public SimpleT9Button(IButton button)
        {
            this.button = button ?? throw new ArgumentNullException(nameof(button));

            pressCount = Symbols.ToDictionary(symbol => symbol, symbol => Symbols.IndexOf(symbol) + 1);
        }

        public int GetPressCount(char symbol) =>
            pressCount.TryGetValue(symbol, out var count) ? count : -1;
    }
}