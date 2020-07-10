using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.T9.Contract;

namespace TestTasks.T9
{
    public class SimpleButton : IButton
    {
        public char Label { get; }

        public IReadOnlyList<char> Symbols { get; }

        public SimpleButton(char label, IEnumerable<char> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols));

            Symbols = symbols.ToArray();

            if (Symbols.Distinct().Count() != Symbols.Count)
                throw new ArgumentException($"Duplicates are not allowed in '{nameof(symbols)}'.");

            Label = label;
        }
    }
}