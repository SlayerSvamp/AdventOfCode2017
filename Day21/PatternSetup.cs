using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    class PatternSetup
    {
        int? _size = null;
        public int Size => _size ?? (_size = (int)Math.Sqrt(Data.Length)).Value;
        public string Data { get; set; }
        List<string> GetPermutations()
        {
            var subPatterns = new List<string>();

            for (int flip = 0; flip < 2; flip++, Data = string.Concat(ReversedLines()))
                for (int i = 0; i < 4; i++, Rotate()) //all rotations
                    subPatterns.Add(Data);

            return subPatterns;
        }
        IEnumerable<string> ReversedLines()
        {
            for (int y = Size - 1; y >= 0; y--)
                yield return Data.Substring(y * Size, Size);
        }
        void Rotate()
        {
            Data = string.Concat(
                Data.Select((c, i) => new { y = i % Size, x = Size - (i / Size) - 1, c })
                .OrderBy(x => x.y)
                .ThenBy(x => x.x)
                .Select(x => x.c));
        }
        public static IEnumerable<Rule> GetRules(string raw)
        {
            var parts = raw.Split(' ');
            var setup = new PatternSetup { Data = parts[0] };
            return setup.GetPermutations()
                .Select(x => new Rule
                {
                    Size = setup.Size,
                    Input = x,
                    Output = parts[2]
                });
        }
    }
}