using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    class Program
    {
        static IEnumerable<List<(int, int)>> StrongestBridge(IEnumerable<(int, int)> components, List<(int, int)> bridge)
        {
            var alternatives = new List<List<(int, int)>>();
            foreach (var component in components)
            {
                (int, int) newComponent;
                List<(int, int)> newBridge;

                if (bridge.Last().Item2 == component.Item1)
                    newComponent = component;
                else if (bridge.Last().Item2 == component.Item2)
                    newComponent = (component.Item2, component.Item1);
                else
                    continue;

                newBridge = bridge.ToList();
                newBridge.Add(newComponent);

                foreach (var alternative in StrongestBridge(components.Except(new List<(int, int)> { component }), newBridge))
                    alternatives.Add(alternative);
            }

            if (alternatives.Count == 0)
                yield return bridge;
            else
                foreach (var alternative in alternatives)
                    yield return alternative;

        }
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt")
                .Select(x => x.Split('/'))
                .Select(x => x.OrderBy(y => y).ToArray())
                .Select(x => (a: int.Parse(x[0]), b: int.Parse(x[1])))
                .ToList();

            var start = input.Where(x => x.a == 0)
                .Select(x => new List<(int, int)> { x })
                .ToList();

            var bridges = start
                .SelectMany(x => StrongestBridge(input.Except(x), x))
                .ToList();

            var strongest = bridges.Max(x => x.Sum(y => y.Item1 + y.Item2));
            Console.WriteLine($"\n Part 1:\r\n {strongest}");

            var longest = bridges.Max(x => x.Count);
            var strongestLongest = bridges
                .Where(x => x.Count == longest)
                .Select(x => x.Sum(y => y.Item1 + y.Item2))
                .Aggregate((x, y) => x > y ? x : y);
            Console.WriteLine($"\n Part 2:\r\n {strongestLongest}");

            Console.ReadLine();
        }
    }
}
