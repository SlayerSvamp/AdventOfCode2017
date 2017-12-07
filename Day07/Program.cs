using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    class Program
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        private int? totalWeight = null;
        public int TotalWeight { get => totalWeight ?? GetSetTotalWeight(); }

        public bool Balanced { get { return !Balancing.Any() || Balancing.All(x => x.TotalWeight == Balancing.First().TotalWeight); } }
        public List<Program> Balancing { get; set; } = new List<Program>();
        public Program BalancingOn { get; set; } = null;
        private int GetSetTotalWeight()
        {
            var weight = Weight;
            foreach (var program in Balancing)
                weight += program.TotalWeight;

            totalWeight = weight;
            return weight;
        }

    }
    class Application
    {
        static List<Program> Initialize(out List<Program> programs)
        {
            var input = File.ReadAllLines("input.txt").Select(x => x.Split(' '));

            programs = input.Select(x => new Program
            {
                Name = x[0],
                Weight = int.Parse(x[1].Trim('(', ')'))
            }).ToList();

            foreach (var parts in input.Where(x => x.Length > 2))
            {
                var program = programs.First(x => x.Name == parts[0]);
                for (int i = 3; i < parts.Length; i++)
                {
                    var sub = programs.First(x => x.Name == parts[i].Trim(','));
                    program.Balancing.Add(sub);
                    sub.BalancingOn = program;
                }
            }
            return programs;
        }
        static Program Part1(List<Program> programs)
        {
            var program = programs.First();
            while (program.BalancingOn != null)
                program = program.BalancingOn;
            Console.WriteLine();
            Console.WriteLine(" Part 1:");
            Console.WriteLine($" {program.Name}");
            Console.WriteLine();
            return program;
        }
        static Program GetUnbalanced(Program program)
        {
            if (program.Balanced)
                return null;
            if (program.Balancing.Count > 2)
            {
                var unbalanced = program.Balancing.GroupBy(x => x.TotalWeight).Single(x => x.Count() == 1).Single();
                var ub2 = GetUnbalanced(unbalanced);
                if (ub2 == null)
                    return unbalanced;
                else
                    return ub2;
            }
            else
            {
                return program.Balancing.Select(x => GetUnbalanced(x)).Single(x => !x.Balanced);
            }
        }
        static void Part2(Program bottom)
        {
            var unbalanced = GetUnbalanced(bottom);

            var diff = unbalanced.BalancingOn.Balancing.First(x => x != unbalanced).TotalWeight - unbalanced.TotalWeight;
            Console.WriteLine(" Part 2:");
            Console.WriteLine($" {unbalanced.Name} weighs {unbalanced.Weight}, but should weigh {Math.Abs(diff)} {(diff > 0 ? "more" : "less")}, at a total of {unbalanced.Weight + diff}");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            Initialize(out List<Program> programs);

            var bottom = Part1(programs);

            Part2(bottom);
        }
    }
}
