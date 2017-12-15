using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    static class Program
    {
        public static IEnumerable<byte> EnumerateList(this List<byte> list)
        {
            for (var i = 0; ; i++)
                yield return list[i % 256];
        }
        static void Encrypt(List<byte> list, IEnumerable<byte> lengths, int loops = 1)
        {

            int currentPosition = 0;
            int skipSize = 0;

            for (int loop = 0; loop < loops; loop++)
            {
                foreach (var length in lengths)
                {
                    //reverse
                    var tempArr = list.EnumerateList().Skip(currentPosition % 256).Take(length).Reverse().ToArray();
                    for (int i = 0; i < length; i++)
                        list[(currentPosition + i) % 256] = tempArr[i];
                    //move && increase
                    currentPosition += length + skipSize++;
                }
            }
        }
        public static string GetHash(string value)
        {
            var lengths = value.Select(x => (byte)x).ToList();
            lengths.AddRange(new byte[] { 17, 31, 73, 47, 23 });
            var sparse = Enumerable.Range(0, 256).Select(x => (byte)x).ToList();
            Encrypt(sparse, lengths.ToArray(), 64);

            var dense = new List<byte>();
            for (int i = 0; i < 16; i++)
                dense.Add(sparse.Skip(i * 16).Take(16).Aggregate((a, b) => (byte)(a ^ b)));
            return dense.Aggregate("", (a, b) => a + b.ToString("x2"));
        }

        static void Main(string[] args)
        {
            // part 1 is a straight up copy of day 10
            var input = "amgozmfv";
            var rows = new StringBuilder();
            var squares = new Dictionary<int, int[]>();
            for (int y = 0; y < 128; y++)
            {
                var hash = GetHash($"{input}-{y}");
                var row = hash.Aggregate("", (a, b) => $"{a}{Convert.ToString(Convert.ToInt32($"{b}", 16), 2).PadLeft(4, '0')}").Replace("1", "#").Replace("0", ".");

                for (int x = 0; x < 128; x++)
                {
                    if (row[x] == '.')
                        continue;
                    var cell = x + (y << 8);
                    squares[cell] = new int[] { cell + 1, cell - 1, cell + 256, cell - 256 };
                }

                rows.Append(row);
            }

            foreach (var square in squares.Keys.ToArray())
                squares[square] = squares[square].Where(x => squares.ContainsKey(x)).ToArray();

            //Part 2 is a straight up copy of day 12
            var regions = new List<List<int>>();

            foreach (var square in squares.Keys)
            {
                if (regions.Any(x => x.Contains(square)))
                    continue;

                var region = new List<int> { square };
                var coordinates = new List<int> { square };
                

                while (coordinates.Any())
                {
                    var adjacent = coordinates.SelectMany(x => squares[x]).ToArray();
                    coordinates.Clear();
                    foreach (var id in adjacent)
                        if (!region.Contains(id))
                        {
                            region.Add(id);
                            coordinates.Add(id);
                        }
                }

                regions.Add(region);
            }


            Console.WriteLine($"\n Part 1:\r\n {rows.ToString().Count(x => x == '#')}");
            Console.WriteLine($"\n Part 2:\r\n {regions.Count}");

            Console.ReadLine();
        }
    }
}
