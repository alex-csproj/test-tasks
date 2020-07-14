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
            var _symbols = symbols?.ToArray() ?? throw new ArgumentNullException(nameof(symbols));

            if (_symbols.Distinct().Count() != _symbols.Length)
                throw new ArgumentException($"Duplicates are not allowed in '{nameof(symbols)}'.");

            Symbols = _symbols;
            Label = label;
        }
    }
}