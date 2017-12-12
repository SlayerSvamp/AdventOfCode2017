using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    public static class Ext
    {
    }
    enum Direction
    {
        North,
        NorthEast,
        SouthEast,
        South,
        SouthWest,
        NorthWest
    }
    class Program
    {
        static List<Direction> GetDirections()
        {
            return File.ReadAllText("input.txt")
                .Replace("n", "1")
                .Replace("e", "2")
                .Replace("s", "3")
                .Replace("w", "4")
                .Replace("1", "North")
                .Replace("2", "East")
                .Replace("3", "South")
                .Replace("4", "West")
                .Split(',')
                .Select(x => (Direction)Enum.Parse(typeof(Direction), x))
                .ToList();
        }
        static void Main(string[] args)
        {
            var directions = GetDirections();
            var positions = new List<Tuple<int, int>>();
            var x = 0;
            var y = 0;

            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case Direction.North: y--; y--; break;
                    case Direction.NorthEast: y--; x++; break;
                    case Direction.SouthEast: y++; x++; break;
                    case Direction.South: y++; y++; break;
                    case Direction.SouthWest: y++; x--; break;
                    case Direction.NorthWest: y--; x--; break;
                }
                positions.Add(new Tuple<int, int>(x, y));
            }

            var distances = positions
                .Select(position =>
                {
                    x = Math.Abs(position.Item1);
                    y = Math.Abs(position.Item2);
                    var steps = 0;
                    while (x != 0 || y != 0)
                    {
                        if (x > 0)
                        {
                            x--;
                            if (y > 0)
                                y--;
                            else
                                y++;
                        }
                        else if (y > 0)
                        {
                            y--;
                            y--;
                        }
                        steps++;
                    }
                    return steps;
                })
                .ToList();

            Console.WriteLine($"\n Part 1:\r\n {distances.Last()}\r\n");
            Console.WriteLine($" Part 2:\r\n {distances.Max()}");
            Console.ReadLine();
        }
    }
}
