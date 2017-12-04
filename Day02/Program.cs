using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt")
                .Select(x => x.Split('\t'))
                .Select(x => x.Select(y => int.Parse(y)));

            Console.WriteLine("Part 1:");
            Console.WriteLine(input.Select(x => x.Max() - x.Min()).Sum());
            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine(input.Select(row =>
                {
                    var numerator = row.First(x => row.Any(y => x / y > 1 && x % y == 0));
                    var denominator = row.First(x => row.Any(y => y / x > 1 && y % x == 0));
                    return numerator / denominator;
                })
                .Sum());


            Console.ReadLine();
        }
    }
}
