using System;
using System.Linq;

namespace TestTasks.T9.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            var count = int.Parse(Console.ReadLine());
            var inputs = Enumerable.Range(0, count)
                                   .Select(i => Console.ReadLine())
                                   .ToArray();

            var converter = new SimpleTextToT9Converter(new SimpleT9Keypad());
            for (var i = 0; i < inputs.Length; i++)
                Console.WriteLine($"Case #{i + 1}: {converter.Convert(inputs[i])}");
        }
    }
}