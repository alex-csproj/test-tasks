using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.Robots.Parse;

namespace TestTasks.Robots.Demo
{
    // Run the program
    // Copy input from the multi-line comment below
    /*
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
     */
    // Past it into application and press 'Enter' twice
    // The output should be as follows
    /*
1 1 E
3 3 N Lost
2 3 S
     */
    class Program
    {
        static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            var area = new RectangularAreaParser().Parse(Console.ReadLine());
            var parser = new SimpleStateParser();

            foreach (var item in GetInputs().ToArray())
            {
                var state = parser.Parse(item.State);
                var robot = new SimpleRobot { Area = area, LastPosition = state.Position, Orientation = state.Orientation };
                var commands = new SimpleCommandParser(robot).Parse(item.Commands);
                foreach (var command in commands)
                    command.Execute();
                Console.WriteLine($"{robot.LastPosition.X} {robot.LastPosition.Y} {robot.Orientation}" +
                                  $"{(robot.Status == Status.Lost ? ' ' + robot.Status.ToString() : string.Empty)}");
            }
        }

        private IEnumerable<(string State, string Commands)> GetInputs()
        {
            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                yield return (line, Console.ReadLine());
        }
    }
}