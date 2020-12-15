using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day03
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle03.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return Num(lines, 3, 1);
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle03.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            int num1 = Num(lines, 1, 1);
            int num2 = Num(lines, 3, 1);
            int num3 = Num(lines, 5, 1);
            int num4 = Num(lines, 7, 1);
            int num5 = Num(lines, 1, 2);

            return num1 * num2 * num3 * num4 * num5;
        }

        private static int Num(string[] lines, int cols, int rows)
        {
            var num = 0;

            var index = 0;
            for (var i = 0; i < lines.Length; i += rows)
            {
                var map = lines[i];

                var full = map;
                for (var j = 0; j < (i / rows); j++)
                    full += map;

                if (full[index] == '#')
                    ++num;

                index += cols;
            }

            return num;
        }
    }
}
