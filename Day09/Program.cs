using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    class Program
    {
        static void print(object t = null)
        {
            Console.WriteLine(t);
        }
        static void Main(string[] args)
        {
            // har försökt efterlikna hur många python-exempel ser ut

            //                  # input
            var t = File.ReadAllText("input.txt");

            var s = 0; //       # score
            var d = 0; //       # depth
            var gc = 0; //      # garbage count
            var i = false; //   # ignore next
            var g = false; //   # in garbage
            foreach (var c in t)
                if (g)
                    if (i)
                        i = false;
                    else if (c == '>')
                        g = false;
                    else if (c == '!')
                        i = true;
                    else
                        gc++;
                else if (c == '{')
                    s += ++d;
                else if (c == '}')
                    d--;
                else if (c == '<')
                    g = true;

            print($"\n Part 1:\r\n {s}\r\n");

            print($" Part 2:\r\n {gc}");

            Console.ReadLine();
        }

    }
}