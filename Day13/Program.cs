using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Layer
    {
        readonly int depth;
        readonly int range;
        public int Severity { get => (Caught(0)) ? depth * range : 0; }
        public bool Caught(int delay) => (depth + delay) % ((range - 1) * 2) == 0;
        public Layer(string[] parts)
        {
            depth = int.Parse(parts[0]);
            range = int.Parse(parts[1]);
        }
    }
    class Program
    {
        static IEnumerable<int> Counter() { for (int i = 0; ; i++) yield return i; }
        static void Main(string[] args)
        {
            var layers = File.ReadAllLines("input.txt").Select(x => new Layer(x.Split(':'))).ToList();
            
            Console.WriteLine($"\n Part 1:\r\n {layers.Sum(x => x.Severity)}");
            Console.WriteLine($"\n Part 2:\r\n {Counter().First(x => !layers.Any(y => y.Caught(x)))}");

            Console.ReadLine();
        }
    }
}
