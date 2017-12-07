using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var jumps = File.ReadAllLines("input.txt").Select(x => int.Parse(x)).ToList();
            var steps = 0;
            for (int i = 0; i >= 0 && i < jumps.Count;)
            {
                // change comment line below between //* and /* to change between Part 1 and Part 2
                /*
                i += jumps[i]++;
                /*/
                var index = i;
                i += jumps[index];
                jumps[index] += (jumps[index] > 2) ? -1 : 1;
                //*/
                steps++;
            }
            Console.WriteLine(steps);
            Console.ReadLine();
        }
    }
}
