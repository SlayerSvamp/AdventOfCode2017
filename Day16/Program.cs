using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    enum Move { Spin, Exchange, Partner }
    class DanceMove
    {
        public Move Move { get; }
        public Action<List<char>> Dance { get; set; }
        public int Value1 { get; }
        public int Value2 { get; }
        private void Swap(List<char> progs, int a, int b)
        {
            var temp = progs[a];
            progs[a] = progs[b];
            progs[b] = temp;
        }
        public DanceMove(string raw)
        {
            switch (raw.First())
            {
                case 's':
                    Move = Move.Spin;
                    Value1 = int.Parse(raw.Substring(1));
                    Dance = (progs) =>
                    {
                        var length = progs.Count - Value1;
                        for (int i = 0; i < length; i++)
                            progs.Add(progs[i]);
                        progs.RemoveRange(0, length);

                    };
                    break;
                case 'x':
                    Move = Move.Exchange;
                    var ex = raw.Substring(1).Split('/').Select(x => int.Parse(x)).ToList();
                    Value1 = ex[0];
                    Value2 = ex[1];
                    Dance = (progs) => Swap(progs, Value1, Value2);
                    break;
                case 'p':
                    Move = Move.Partner;
                    Dance = (progs) =>
                    {
                        var a = progs.IndexOf(raw[1]);
                        var b = progs.IndexOf(raw[3]);
                        Swap(progs, a, b);
                    };
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var programs = Enumerable.Range('a', 'p' - 'a' + 1).Select(x => (char)x).ToList();
            var moves = File.ReadAllText("input.txt").Split(',').Select(x => new DanceMove(x)).ToList();

            var part1 = programs.ToList();
            foreach (var move in moves)
                move.Dance(part1);

            var concat = string.Concat(part1);
            Console.WriteLine($"\n Part 1:\r\n {concat}\n");
            
            var start = 0;
            var count = 0;
            var seen = new List<string> { string.Concat(programs) };
            for (int i = 0; ; i++)
            {
                foreach (var move in moves)
                    move.Dance(programs);

                var next = string.Concat(programs);
                if (seen.Contains(next))
                {
                    start = seen.IndexOf(next);
                    count = i - start + 1;
                    break;
                }
                seen.Add(next);
            }

            var index = (1000 * 1000 * 1000 - start) % count;
            concat = string.Concat<char>(seen[index]);
            Console.WriteLine($"\r Part 2:\r\n {concat}");

            Console.ReadLine();
        }
    }
}
