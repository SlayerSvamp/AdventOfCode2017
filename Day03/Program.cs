using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public bool IsAdjacent(int x, int y)
        {
            return !(x < X - 1 || x > X + 1 || y < Y - 1 || y > Y + 1);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var input = 325489;
            Console.WriteLine();

            var points = new List<Point>
            {
                new Point
                {
                    X = 0,
                    Y = 0,
                    Value = 1
                }
            };
            var _x = 0;
            var _y = 0;

            Action add = () =>
            {
                if (points == null)
                    return;
                var value = points.Where(x => x.IsAdjacent(_x, _y)).Sum(x => x.Value);

                if (value > input)
                {
                    Console.WriteLine("Part 2:");
                    Console.WriteLine(value);
                    Console.WriteLine();
                    points = null;
                    return;
                }
                points.Add(new Point
                {
                    X = _x,
                    Y = _y,
                    Value = value
                });
            };
            var steps = 1;
            for (int i = 1; ;)
            {
                for (int n = 0; n < steps; n++)
                {
                    i++;
                    _x++;
                    add();
                    if (i == input)
                        goto afterLoop;
                }
                for (int n = 0; n < steps; n++)
                {
                    i++;
                    _y--;
                    add();
                    if (i == input)
                        goto afterLoop;
                }
                steps++;
                for (int n = 0; n < steps; n++)
                {
                    i++;
                    _x--;
                    add();
                    if (i == input)
                        goto afterLoop;
                }
                for (int n = 0; n < steps; n++)
                {
                    i++;
                    _y++;
                    add();
                    if (i == input)
                        goto afterLoop;
                }
                steps++;
            }

            afterLoop:

            var maxSteps = Math.Abs(_x) + Math.Abs(_y);
            Console.WriteLine("Part 1:");
            Console.WriteLine($"[{_x},{_y}]");
            Console.WriteLine($"{maxSteps} steps");
            Console.WriteLine();


            Console.ReadLine();
        }
    }
}
