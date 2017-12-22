using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    enum Part { One, Two }
    enum Mode
    {
        Send,
        Set,
        Add,
        Multiply,
        Modulus,
        Recieve,
        Jump
    }
    class Instruction
    {
        public Mode Mode { get; set; }
        char register1 { get; }
        char register2 { get; set; } = '0';
        long? value1 { get; } = null;
        long? value2 { get; } = null;
        public bool Has2Values => value2 != null || register2 != '0';
        public long Value1
        {
            get => value1 ?? Registers[register1];
            set => Registers[register1] = value;
        }
        public long Value2 => value2 ?? Registers[register2];
        public Dictionary<char, long> Registers { get; set; }
        public Instruction(string[] parts, Dictionary<char, long> registers)
        {
            Registers = registers;
            register1 = parts[1].Single();
            switch (parts[0])
            {
                case "snd": Mode = Mode.Send; break;
                case "set": Mode = Mode.Set; break;
                case "add": Mode = Mode.Add; break;
                case "mul": Mode = Mode.Multiply; break;
                case "mod": Mode = Mode.Modulus; break;
                case "rcv": Mode = Mode.Recieve; break;
                case "jgz": Mode = Mode.Jump; break;
            }

            if (long.TryParse(parts[1], out long value))
                value1 = value;
            else
            {
                register1 = parts[1].Single();
                Registers[register1] = 0;
            }

            if (parts.Length > 2)
            {
                if (long.TryParse(parts[2], out value))
                    value2 = value;
                else
                {
                    register2 = parts[2].Single();
                    Registers[register2] = 0;
                }
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var registers = new Dictionary<char, long>();
            var input = File.ReadAllLines("input.txt")
            //var input = "snd 1\nsnd 2\nsnd p\nrcv a\nrcv b\nrcv c\nrcv d".Split('\n')// test
                .Select(x => x.Split(' '))
                .ToList();

            var program = input.Select(x => new Instruction(x, registers)).ToList();

            long last = 0;
            var loop = true;
            for (int i = 0; loop; i++)
            {
                var instruction = program[i];
                switch (instruction.Mode)
                {
                    case Mode.Set: instruction.Value1 = instruction.Value2; break;
                    case Mode.Add: instruction.Value1 += instruction.Value2; break;
                    case Mode.Multiply: instruction.Value1 *= instruction.Value2; break;
                    case Mode.Modulus: instruction.Value1 %= instruction.Value2; break;
                    case Mode.Jump: if (instruction.Value1 > 0) i += (int)instruction.Value2 - 1; break;
                    case Mode.Send:
                        last = instruction.Value1;
                        break;
                    case Mode.Recieve:
                        if (instruction.Value1 != 0)
                            Console.WriteLine($"\n Part 1:\r\n {last}");
                        i = -1;
                        loop = false;
                        break;
                }
            }

            registers = new Dictionary<char, long>();
            var registers2 = new Dictionary<char, long>();
            program = input.Select(x => new Instruction(x, registers)).ToList();
            var program2 = input.Select(x => new Instruction(x, registers2)).ToList();
            registers2['p'] = 1;

            var queue = new Queue<long>();
            var queue2 = new Queue<long>();
            var count = 0;
            Console.WriteLine();
            var i1 = Run(ref count, 0, program, queue, queue2, true);
            var i2 = Run(ref count, 0, program2, queue2, queue);
            do
            {
                if (i1 >= 0 && queue.Any())
                    i1 = Run(ref count, i1, program, queue, queue2);
                else
                    break;
                if (i2 >= 0 && queue2.Any())
                    i2 = Run(ref count, i2, program2, queue2, queue, true);
                else
                    break;

            }
            while (true);

            Console.ReadLine();
        }

        static int Run(ref int count, int i, List<Instruction> program, Queue<long> read, Queue<long> write, bool isInteresting = false)
        {
            for (; ; i++)
            {
                if (i < 0 || i >= program.Count)
                    return -1;
                var instruction = program[i];
                switch (instruction.Mode)
                {
                    case Mode.Set: instruction.Value1 = instruction.Value2; break;
                    case Mode.Add: instruction.Value1 += instruction.Value2; break;
                    case Mode.Multiply: instruction.Value1 *= instruction.Value2; break;
                    case Mode.Modulus: instruction.Value1 %= instruction.Value2; break;
                    case Mode.Jump: if (instruction.Value1 > 0) i += (int)instruction.Value2 - 1; break;
                    case Mode.Send:
                        write.Enqueue(instruction.Value1);
                        if (isInteresting)
                        {
                            count++;
                            Console.Write($"\r Part 2:\r\n {count}");
                            Console.CursorTop--;
                        }
                        break;
                    case Mode.Recieve:
                        if (read.Count == 0)
                            return i;

                        instruction.Value1 = read.Dequeue();
                        break;
                }
            }
        }
    }
}
