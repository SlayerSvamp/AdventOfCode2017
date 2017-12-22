using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var clock = new Stopwatch();
            clock.Start();
            var steps = 301;
            var current = 0;
            var buffer = new List<int> { 0 };
            for (int i = 1; i <= 2017; i++)
            {
                current = current + steps + 1;
                current %= buffer.Count;
                buffer.Insert(current, i);
            }

            current++;
            clock.Stop();
            Console.WriteLine($"\n Part 1:\r\n {buffer[current]}");
            Console.WriteLine($" - {clock.ElapsedMilliseconds} ms");

            clock.Restart();
            current = 0;
            var observed = 0;
            for (int length = 1; length <= 50000000; length++)
            {
                current = (current + steps + 1) % length;
                if (current == 0) // zero is always first
                    observed = length;
            }
            clock.Stop();
            Console.WriteLine($"\n Part 2:\r\n {observed}");
            Console.WriteLine($" - {clock.ElapsedMilliseconds} ms");

            if (observed <= 5669688)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Too low!!");
            }

            Console.ReadLine();
        }
    }
}