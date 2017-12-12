using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    enum Action
    {
        Increase,
        Decrease
    }
    class Condition
    {
        public string Register { get; set; }
        public string Operator { get; set; } 
        public int Value { get; set; }
        public Condition(string raw)
        {
            var parts = raw.Split(' ');
            Register = parts[0];
            Operator = parts[1];
            Value = int.Parse(parts[2]);
        }
        public bool Evaluate(IDictionary<string, int> registers)
        {
            var value = registers[Register];
            switch (Operator)
            {
                case "==":
                    return value == Value;
                case "<=":
                    return value <= Value;
                case ">=":
                    return value >= Value;
                case "<":
                    return value < Value;
                case ">":
                    return value > Value;
                case "!=":
                    return value != Value;
                default:
                    throw new ArgumentException($"You missed a spot! '{Operator}'");
            }
        }
    }
    class Instruction
    {
        public string Register { get; set; }
        public Action Action { get; set; }
        public int Value { get; set; }
        public Condition Condition { get; set; }

    }
    static 
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt")
                .Select(x => new { parts = x.Split(' '), condition = x.Substring(x.IndexOf(" if ") + 4) })
                .Select(x => new Instruction
                {
                    Register = x.parts[0],
                    Action = x.parts[1] == "inc" ? Action.Increase : Action.Decrease,
                    Value = int.Parse(x.parts[2]),
                    Condition = new Condition(x.condition)

                }).ToList();

            var registers = instructions.Select(x => x.Register).Distinct().ToDictionary(x => x, x => 0);
            var max = 0;
            foreach(var instruction in instructions)
                if (instruction.Condition.Evaluate(registers))
                {
                    registers[instruction.Register] += instruction.Value * (instruction.Action == Action.Increase ? 1 : -1);
                    max = Math.Max(registers[instruction.Register], max);
                }

            Console.WriteLine();
            Console.WriteLine(" Part 1:");
            Console.WriteLine($" {registers.Values.Max()}");
            Console.WriteLine();

            Console.WriteLine(" Part 2:");
            Console.WriteLine($" {max}");
            Console.ReadLine();
        }
    }
}
