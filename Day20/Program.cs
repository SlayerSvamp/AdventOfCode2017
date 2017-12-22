using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        static List<Particle> GetOriginalState(IEnumerable<string[]> input)
        {
            return input.Select((x, i) => new Particle
            {
                ID = i,
                Position = new Vector(x[0].Trim(',')),
                Velocity = new Vector(x[1].Trim(',')),
                Acceleration = new Vector(x[2])
            })
                .ToList();
        }
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(x => x.Split(' '));
            var particles = GetOriginalState(input);

            for (int i = 0; i < 330; i++)
                foreach (var particle in particles)
                    particle.Tick();

            Console.WriteLine($"\n Part 1:\r\n {particles.OrderBy(x => x.ManhattanDistance).First().ID}");

            var clock = new Stopwatch();
            Console.Write($"\n Calculating...");
            clock.Start();
            particles = GetOriginalState(input);
            for (int i = 0; i < 40; i++)
            {
                foreach (var particle in particles)
                    particle.Tick();

                var collided = new List<Particle>();
                foreach (var particle in particles)
                    if (particles.Any(x => x.Position == particle.Position && x != particle))
                        collided.Add(particle);

                foreach (var particle in collided)
                    particles.Remove(particle);
            }
            clock.Stop();

            Console.WriteLine($"\r Part 2:       \r\n {particles.Count}\r\n - {clock.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }

    }
}