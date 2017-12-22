using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = ".#...####";

            var rules = File.ReadAllLines("input.txt")
                .Select(x => x.Replace("/", ""))
                .SelectMany(PatternSetup.GetRules)
                .GroupBy(x => x.Input)
                .Select(x => x.First())
                .ToList();

            var clock = new Stopwatch();
            clock.Start();
            for (int _i = 1; _i <= 18; _i++)
            {
                var size = (int)Math.Sqrt(grid.Length);
                var divBy = size % 2 == 0 ? 2 : 3;
                var newGrid = new StringBuilder();
                for (int y = 0; y < size; y += divBy)
                {
                    var replacements = new List<string>();
                    for (int x = 0; x < size; x += divBy)
                    {
                        var partial = new StringBuilder();
                        for (int i = 0; i < divBy; i++)
                            partial.Append(grid.Substring(x + (y + i) * size, divBy));

                        var part = partial.ToString();
                        var match = rules.First(rule => rule.Input == part).Output;
                        replacements.Add(match);
                    }
                    for (int i = 0; i <= divBy; i++)
                        foreach (var replacement in replacements)
                            newGrid.Append(replacement.Substring(i * divBy + i, divBy + 1));
                }

                grid = newGrid.ToString();

                if (_i == 5)
                {
                    Console.WriteLine($"\n Part 1:\r\n {grid.Count(x => x == '#')}\r\n - {clock.ElapsedMilliseconds} ms");
                    clock.Restart();
                }
            }

            Console.WriteLine($"\n Part 2:\r\n {grid.Count(x => x == '#')}\r\n - {clock.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }

    }
}