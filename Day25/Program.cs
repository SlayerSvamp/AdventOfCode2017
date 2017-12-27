using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            var limit = 12667664; // steps until diagnostic checksum
            var cursor = 0;
            var state = 'A';
            var tape = new Dictionary<int, byte>();
            Func<byte> read = () => tape.ContainsKey(cursor) ? tape[cursor] : (tape[cursor] = 0);
            Action<byte> write = (value) => tape[cursor] = value;
            Action flip = () => tape[cursor] ^= 1;
            Func<bool> zero = () => read() == 0;

            Func<byte, int, char, bool> set = (_value, _direction, _state) =>
            {
                write(_value);
                cursor += _direction;
                state = _state;
                return true;
            };

            var states = new Dictionary<char, Func<bool>>();
            states['A'] = () => zero() ? set(1, 1, 'B') : set(0, -1, 'C');
            states['B'] = () => zero() ? set(1, -1, 'A') : set(1, 1, 'D');
            states['C'] = () => zero() ? set(0, -1, 'B') : set(0, -1, 'E');
            states['D'] = () => zero() ? set(1, 1, 'A') : set(0, 1, 'B');
            states['E'] = () => zero() ? set(1, -1, 'F') : set(1, -1, 'C');
            states['F'] = () => zero() ? set(1, 1, 'D') : set(1, 1, 'A');

            for (int i = 0; i < limit; i++)
                states[state]();

            Console.WriteLine($"\n Part 1:\r\n {tape.Values.Count(x => x == 1)}");

            Console.ReadLine();
        }
    }
}
