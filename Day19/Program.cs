using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    enum Direction
    {
        Up = -256,
        Right = 1,
        Down = 256,
        Left = -1
    }

    class Program
    {
        static void Main(string[] args)
        {
            var points = File.ReadAllLines("input.txt")
                .SelectMany((row, y) => row.Select((value, x) => new { key = x + (y << 8), value }))
                .ToDictionary(x => x.key, x => x.value);

            var direction = Direction.Down;
            var key = points.Where(x => x.Key < 256).Single(x => x.Value != ' ').Key;
            Func<char> current = () => points[key];


            var path = new StringBuilder();
            for (var count = 1; ; count++)
            {
                key += (int)direction;

                if (current() >= 'A' && current() <= 'Z')
                    path.Append(current());
                else if (current() == '+')
                {
                    if (direction == Direction.Up || direction == Direction.Down)
                    {
                        if (points[key + (int)Direction.Right] != ' ')
                            direction = Direction.Right;
                        else
                            direction = Direction.Left;
                    }
                    else if (points[key + (int)Direction.Down] != ' ')
                        direction = Direction.Down;
                    else
                        direction = Direction.Up;
                }
                if (current() == ' ')
                {
                    Console.WriteLine($"\r\n Part 1:\r\n {path}");
                    Console.WriteLine($"\r\n Part 2:\r\n {count}");

                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}
