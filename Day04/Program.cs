using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    class Program
    {
        static void Run(int part, List<IEnumerable<string>> phrases)
        {
            foreach (var phrase in phrases.ToList())
            {
                var valid = true;
                foreach (var word in phrase)
                {
                    if (phrase.Count(x => x == word) > 1)
                        valid = false;
                }

                if (!valid)
                    phrases.Remove(phrase);
            }

            Console.WriteLine();
            Console.WriteLine($" Part {part}:");
            Console.WriteLine($" {phrases.Count}");
        }
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(x => x.Split(' ').AsEnumerable());

            var phrases = input.ToList();
            Run(1, phrases);

            phrases = input.Select(x => x.Select(y => string.Concat(y.OrderBy(z => z)))).ToList();
            Run(2, phrases);

            Console.ReadLine();
        }
    }
}
