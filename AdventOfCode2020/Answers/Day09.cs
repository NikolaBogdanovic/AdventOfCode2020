using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Answers
{
    public static class Day09
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle09.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var preamble = new List<int>(25);

            var index = 0;
            foreach (var data in lines)
            {
                var num = int.Parse(data);

                if (index >= 25)
                {
                    if (!Xmas(preamble, num))
                        return num;

                    preamble.RemoveAt(0);
                }

                preamble.Add(num);
                ++index;
            }

            return -1;
        }

        private static bool Xmas(List<int> preamble, int num)
        {
            foreach (var i in preamble)
            {
                foreach (var j in preamble)
                {
                    if (i != j && i + j == num)
                        return true;
                }
            }

            return false;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle09.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var preamble = new List<int>(25);

            int error = 0;

            var index = 0;
            foreach (var data in lines)
            {
                var num = int.Parse(data);

                if (index >= 25)
                {
                    if (!Xmas(preamble, num))
                    {
                        error = num;
                        break;
                    }

                    preamble.RemoveAt(0);
                }

                preamble.Add(num);
                ++index;
            }

            Tuple<int, int> range = null;

            for (var i = 0; i < lines.Length; i++)
            {
                var num = int.Parse(lines[i]);

                for (var j = i + 1; j < lines.Length; j++)
                {
                    num += int.Parse(lines[j]);

                    if (num >= error)
                    {
                        range = Tuple.Create(i, j);
                        break;
                    }
                }

                if (num == error)
                    break;
            }

            if (range != null)
            {
                var weakness = lines
                    .Skip(range.Item1)
                    .Take(range.Item2 - range.Item1)
                    .Select(int.Parse)
                    .ToArray();

                return weakness.Min() + weakness.Max();
            }

            return -1;
        }
    }
}
