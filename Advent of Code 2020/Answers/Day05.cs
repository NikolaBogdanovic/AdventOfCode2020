using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day05
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle05.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var max = -1;

            foreach (var seat in lines)
            {
                var range = Tuple.Create(0, 127);

                foreach (var bin in seat.Take(7))
                {
                    range = HalfRange(bin == 'F', range);
                }

                var row = range.Item1;

                range = Tuple.Create(0, 7);

                foreach (var bin in seat.Skip(7))
                {
                    range = HalfRange(bin == 'L', range);
                }

                var col = range.Item1;

                var id = (row * 8) + col;
                if (id > max)
                {
                    max = id;
                }
            }

            return max;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle05.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var all = new List<int>(128 * 8);
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    all.Add((i * 8) + j);
                }
            }

            foreach (var seat in lines)
            {
                var range = Tuple.Create(0, 127);

                foreach (var bin in seat.Take(7))
                {
                    range = HalfRange(bin == 'F', range);
                }

                var row = range.Item1;

                range = Tuple.Create(0, 7);

                foreach (var bin in seat.Skip(7))
                {
                    range = HalfRange(bin == 'L', range);
                }

                var col = range.Item1;

                var id = (row * 8) + col;
                all.Remove(id);
            }

            for (var i = 1; i < all.Count; i++)
            {
                var id = all[i];

                if (id > all[i - 1] + 1 && id < all[i + 1] - 1)
                    return id;
            }

            return -1;
        }

        private static Tuple<int, int> HalfRange(bool upper, Tuple<int, int> range)
        {
            if (upper)
            {
                return Tuple.Create(range.Item1, range.Item1 + ((range.Item2 - range.Item1) / 2));
            }

            return Tuple.Create(range.Item1 + ((range.Item2 - range.Item1) / 2) + 1, range.Item2);
        }
    }
}
