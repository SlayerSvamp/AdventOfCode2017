using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Generator : IEnumerator<int>, IEnumerable<int>
    {
        public int PickyDivisor { get; set; }
        public int Factor { get; set; }
        public int _startValue;
        public int StartValue
        {
            get => _startValue;
            set => _startValue = Current = value;
        }
        public int Current { get; set; }
        public ushort Compared => (ushort)Current;
        object IEnumerator.Current => Current;

        public void Dispose() { /* nothing to dispose */ }

        public bool MoveNext()
        {
            Current = (int)(((long)Current * Factor) % int.MaxValue);
            return true;
        }
        internal bool PickyMoveNext()
        {
            do
                MoveNext();
            while (Current % PickyDivisor > 0);
            return true;
        }

        public void Reset() => Current = StartValue;
        public IEnumerator<int> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var A = new Generator
            {
                StartValue = 883,
                Factor = 16807,
                PickyDivisor = 4
            };

            var B = new Generator
            {
                StartValue = 879,
                Factor = 48271,
                PickyDivisor = 8
            };
            
            Console.Write("\n Calculating...");
            var judge = 0;
            for (int i = 0; i < 40000000; i++)
            {
                A.MoveNext();
                B.MoveNext();
                if (A.Compared == B.Compared)
                    judge++;
            }
            Console.WriteLine($"\r Part 1:       \r\n {judge}");

            A.Reset();
            B.Reset();
            Console.Write("\n Calculating...");
            judge = 0;
            for (int i = 0; i < 5000000; i++)
            {
                A.PickyMoveNext();
                B.PickyMoveNext();
                if (A.Compared == B.Compared)
                    judge++;
            }
            Console.WriteLine($"\r Part 2:       \r\n {judge}");

            Console.ReadLine();
        }
    }
}
