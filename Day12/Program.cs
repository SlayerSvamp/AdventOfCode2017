using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var programs = File.ReadAllLines("input.txt")
                .Select(x => x.Replace(",", "").Split(' '))
                .ToDictionary(x => int.Parse(x[0]), x => x.Skip(2).Select(int.Parse).ToArray());

            var groups = new List<List<int>>();

            for (var i = 0; i < programs.Count; i++)
            {
                if (groups.Any(x => x.Contains(i)))
                    continue;

                var connected = new List<int> { i };
                var ids = new List<int> { i };
                var changed = true;

                while (changed)
                {
                    changed = false;
                    var newIds = new List<int>();
                    foreach (var id in ids.SelectMany(x => programs[x]))
                        if (!connected.Contains(id))
                        {
                            connected.Add(id);
                            newIds.Add(id);
                            changed = true;
                        }
                    ids = newIds;
                }

                groups.Add(connected);
            }

            Console.WriteLine($"\n Part 1:\r\n {groups[0].Count}");
            Console.WriteLine($"\n Part 2:\r\n {groups.Count}");
            Console.ReadLine();
        }
    }
}
