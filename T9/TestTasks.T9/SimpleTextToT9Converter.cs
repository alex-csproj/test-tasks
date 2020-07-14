using System;
using System.Text;
using TestTasks.T9.Contract;

namespace TestTasks.T9
{
    public class SimpleTextToT9Converter : ITextToT9Converter
    {
        private readonly IT9Keypad keypad;

        public SimpleTextToT9Converter(IT9Keypad keypad) =>
            this.keypad = keypad ?? throw new ArgumentNullException(nameof(keypad));

        public string Convert(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var sb = new StringBuilder();

            IT9Button prevButton = default;
            foreach (var symbol in text)
            {
                var button = keypad.GetButton(symbol);

                if (ReferenceEquals(prevButton, button))
                    sb.Append(' ');

                var pressCount = button.GetPressCount(symbol);
                for (var i = 0; i < pressCount; i++)
                    sb.Append(button.Label);

                prevButton = button;
            }

            return sb.ToString();
        }
    }
}