using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Day23
{
    public enum Mode { Set, Subtract, Multiply, JumpNotZero }
    public class Variable
    {
        public BigInteger _value;
        readonly char? _register = null;
        public BigInteger Value
        {
            get => _register.HasValue ? Registers[_register.Value] : _value;
            set => Registers[_register.Value] = value;
        }
        public char? Register => _register;
        public Dictionary<char, BigInteger> Registers { get; set; }
        public Variable(string raw, Dictionary<char, BigInteger> registers)
        {
            Registers = registers;
            if (BigInteger.TryParse(raw, out BigInteger value))
                _value = value;
            else
            {
                _register = raw.First();
                Registers[_register.Value] = 0;
            }
        }

    }
    public class Instruction
    {
        public Mode Mode { get; set; }
        public Variable X { get; set; }
        public Variable Y { get; set; }

        public static Mode GetMode(string mode)
        {
            switch (mode)
            {
                case "set": return Mode.Set;
                case "sub": return Mode.Subtract;
                case "mul": return Mode.Multiply;
                case "jnz": return Mode.JumpNotZero;
                default:
                    throw new ArgumentException("mode string was not in the correct format");
            }
        }
        public void Run(ref int index)
        {
            switch (Mode)
            {
                case Mode.Set:
                    X.Value = Y.Value;
                    break;
                case Mode.Subtract:
                    X.Value -= Y.Value;
                    break;
                case Mode.Multiply:
                    X.Value *= Y.Value;
                    break;
                case Mode.JumpNotZero:
                    if (X.Value != 0)
                        index += (int)Y.Value - 1;
                    break;
                default:
                    break;
            }
        }
    }
    class Program
    {
        static void Part1()
        {
            var registers = new Dictionary<char, BigInteger>();
            var instructions = File.ReadAllLines("input.txt")
                .Select(x => x.Split())
                .Select(x => new Instruction
                {
                    Mode = Instruction.GetMode(x[0]),
                    X = new Variable(x[1], registers),
                    Y = new Variable(x[2], registers)
                })
                .ToList();

            var count = 0;
            for (int index = 0; index < instructions.Count; index++)
            {
                if (instructions[index].Mode == Mode.Multiply)
                    count++;

                instructions[index].Run(ref index);
            }

            Console.WriteLine($"\n Part 1:\r\n {count}");

        }
        static List<int> Primes(Func<int, bool> predicate)
        {
            var primes = new List<int>();

            for (int i = 2; predicate(i); i++)
                if (!primes.Any(x => i % x == 0))
                    primes.Add(i);

            return primes;
        }
        static IEnumerable<int> Range(int start, int count, int step)
        {
            for (int i = 0; i < count; i++)
                yield return start + i * step;
        }
        static void Part2()
        {
            //had to convert the code from "assembly" to C#
            //the program finds count of non-primes in range 106500 - 123500 with 17 steps at a time
            //pseudo-code in end of method
            var limit = 123500;
            var primes = Primes(x => x <= limit);
            
            var count = Range(0, 1001, 17)
                .Select(x => x + 106500)
                .Except(primes)
                .Count();

            Console.WriteLine($"\n Part 2:\r\n {count}");
            /*
            
            #pseudo-code:

            c = 123500
            h = 0

            while [b = 106500; b < c ; b += 17]
            (
                while [d = 2, f = 1 ; d < b ; d += 1]
		            while [e = 2 ; e < b ; e += 1]
			            if [d * e == b]
				            f = 0
                    
	            if f == 0
		            count += 1
            )

            ## code is pseudo just to stop the "remove commented out code" remark
            */
        }
        static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.ReadLine();
        }
    }
}
