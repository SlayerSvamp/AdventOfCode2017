using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
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
                    //move
                    currentPosition += length + skipSize;
                    //increase
                    skipSize++;
                }
            }
        }
        static void Main(string[] args)
        {
            //input 206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3
            Func<List<byte>> getList = () => Enumerable.Range(0, 256).Select(x => (byte)x).ToList();
            var list = getList();
            var lengths = new byte[] { 206, 63, 255, 131, 65, 80, 238, 157, 254, 24, 133, 2, 16, 0, 1, 3 };
            Encrypt(list, lengths);

            Console.Write("\n Part 1:\r\n ");
            Console.WriteLine(list.First() * list.Skip(1).First());

            var lengthList = "206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3".Select(x => (byte)x).ToList();
            lengthList.AddRange(new byte[] { 17, 31, 73, 47, 23 });
            lengths = lengthList.ToArray();
            var sparse = getList();
            Encrypt(sparse, lengths, 64);

            var dense = new List<byte>();
            for (int i = 0; i < 16; i++)
                dense.Add(sparse.Skip(i * 16).Take(16).Aggregate((a, b) => (byte)(a ^ b)));

            Console.Write("\n Part 2:\r\n ");
            Console.WriteLine(dense.Aggregate("", (a, b) => a + b.ToString("x2")));
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
