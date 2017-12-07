using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static void Redistribute(List<int> banks)
        {
            var value = banks.Max();
            var index = banks.IndexOf(value);
            banks[index] = 0;
            for (var i = index + 1; value > 0; i++)
            {
                i %= 16;
                banks[i]++;
                value--;
            }
        }
        static void Main(string[] args)
        {
            var banks = new List<int> { 4, 1, 15, 12, 0, 9, 9, 5, 5, 8, 7, 3, 14, 5, 12, 3 };
            var history = new List<List<int>>();
            var loops = 0;
            while (!history.Any(x => x.SequenceEqual(banks)))
            {
                history.Add(banks.ToList());
                Redistribute(banks);
                loops++;
            }

            Console.WriteLine(loops);
            Console.WriteLine(history.Count - history.IndexOf(history.First(x => x.SequenceEqual(banks))));
            Console.ReadLine();
        }
    }
}
