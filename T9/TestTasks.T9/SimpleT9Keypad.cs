using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.T9.Contract;

namespace TestTasks.T9
{
    public class SimpleT9Keypad : IT9Keypad
    {
        private static readonly IReadOnlyList<IT9Button> buttons =
            new[]
            {
                new SimpleButton('2',"abc"),
                new SimpleButton('3',"def"),
                new SimpleButton('4',"ghi"),
                new SimpleButton('5',"jkl"),
                new SimpleButton('6',"mno"),
                new SimpleButton('7',"pqrs"),
                new SimpleButton('8',"tuv"),
                new SimpleButton('9',"wxyz"),
                new SimpleButton('0'," ")
            }
            .Select(button => new SimpleT9Button(button))
            .ToArray();

        private static readonly IReadOnlyDictionary<char, IT9Button> buttonBySymbol =
            buttons
            .SelectMany(button => button.Symbols.Select(symbol => (symbol, button)))
            .ToDictionary(i => i.symbol, i => i.button);

        public IT9Button GetButton(char symbol)
        {
            try
            {
                return buttonBySymbol[symbol];
            }
            catch(KeyNotFoundException)
            {
                throw new ArgumentException($"Button with given symbol '{symbol}' does not exist.");
            }
        }

        public bool TryGetButton(char symbol, out IT9Button button) =>
            buttonBySymbol.TryGetValue(symbol, out button);
    }
}