using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    enum State { Clean = 0, Weakened = 1, Infected = 2, Flagged = 3 }
    enum Direction { Up = 0, Right = 1, Down = 2, Left = 3 }
    static class Program
    {
        static void Turn(ref Direction direction, State state, bool part1)
        {
            if (part1)
                switch (direction)
                {
                    case Direction.Up: direction = state == State.Infected ? Direction.Right : Direction.Left; return;
                    case Direction.Right: direction = state == State.Infected ? Direction.Down : Direction.Up; return;
                    case Direction.Down: direction = state == State.Infected ? Direction.Left : Direction.Right; return;
                    case Direction.Left: direction = state == State.Infected ? Direction.Up : Direction.Down; return;
                }
            switch (state)
            {
                case State.Clean: direction += 3; break;
                case State.Infected: direction += 1; break;
                case State.Flagged: direction += 2; break;
            }
            direction = (Direction)((int)direction % 4);
        }
        static void Run(bool part1)
        {
            var nodes = File.ReadAllLines("input.txt")
                .SelectMany((r, y) => r.Select((c, x) => new { key = (x, y), state = c == '#' ? State.Infected : State.Clean }))
                .ToDictionary(x => x.key, x => x.state);

            var start = (int)Math.Sqrt(nodes.Count) / 2;
            var current = (x: start, y: start);
            var direction = Direction.Up;
            var infects = 0;
            var bursts = part1 ? 10000 : 10000000;
            for (int burst = 0; burst < bursts; burst++)
            {
                if (!nodes.ContainsKey(current))
                    nodes[current] = State.Clean;

                Turn(ref direction, nodes[current], part1);

                if (part1)
                    nodes[current] ^= State.Infected; // toggles: infected / clean
                else if (++nodes[current] > State.Flagged)
                    nodes[current] = State.Clean;

                if (nodes[current] == State.Infected)
                    infects++;

                switch (direction)
                {
                    case Direction.Up: current = (x: current.x, y: current.y - 1); break;
                    case Direction.Right: current = (x: current.x + 1, y: current.y); break;
                    case Direction.Down: current = (x: current.x, y: current.y + 1); break;
                    case Direction.Left: current = (x: current.x - 1, y: current.y); break;
                }
            }
            Console.WriteLine($"\n Part {(part1 ? '1' : '2')}:\r\n {infects}");
        }
        static void Main(string[] args)
        {
            Run(part1: true);
            Run(part1: false);
            Console.ReadLine();
        }
    }
}