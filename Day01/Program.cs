using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Select(x => x - '0').ToList();

            var sum = 0;
            var sum2 = 0;
            var half = input.Count / 2;
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == input[(i + 1) % input.Count])
                    sum += input[i];
                if (input[i] == input[(i + half) % input.Count])
                    sum2 += input[i];
            }
            Console.WriteLine();
            Console.WriteLine($" Part 1: {sum}");
            Console.WriteLine();
            Console.WriteLine($" Part 2: {sum2}");
            Console.ReadLine();
        }
    }
}
