using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day11
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle11.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var seats = lines.ToArray();

            int last;
            do
            {
                last = 0;

                var cache = new Dictionary<int, Dictionary<int, bool>>(seats.Length);

                for (var i = 0; i < seats.Length; i++)
                {
                    var row = seats[i];

                    cache.Add(i, new Dictionary<int, bool>(row.Length));

                    for (var j = 0; j < row.Length; j++)
                    {
                        var col = row[j];

                        cache[i].Add(j, col == '#');
                    }
                }

                for (var i = 0; i < seats.Length; i++)
                {
                    var row = seats[i].ToCharArray();
                    for (var j = 0; j < row.Length; j++)
                    {
                        var col = row[j];
                        if (col == '.')
                            continue;

                        var num = Adjecent(cache, i, j);

                        if (col == 'L' && num == 0)
                        {
                            row[j] = '#';

                            ++last;
                        }
                        else if (col == '#' && num >= 4)
                        {
                            row[j] = 'L';

                            ++last;
                        }
                    }

                    seats[i] = new string(row);
                }
            }
            while (last > 0);

            var occupied = seats.Sum(x => x.Count(y => y == '#'));

            return occupied;
        }

        private static int Adjecent(Dictionary<int, Dictionary<int, bool>> cache, int i, int j)
        {
            var num = 0;

            var rows = cache.Count - 1;
            var cols = cache[i].Count - 1;

            if (i > 0)
            {
                var prev = cache[i - 1];

                if (j > 0 && prev[j - 1])
                    ++num;

                if (prev[j])
                    ++num;

                if (j < cols && prev[j + 1])
                    ++num;
            }

            var dict = cache[i];

            if (j > 0 && dict[j - 1])
                ++num;

            if (j < cols && dict[j + 1])
                ++num;

            if (i < rows)
            {
                var next = cache[i + 1];

                if (j > 0 && next[j - 1])
                    ++num;

                if (next[j])
                    ++num;

                if (j < cols && next[j + 1])
                    ++num;
            }

            return num;
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle11.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var seats = lines.ToArray();

            int last;
            do
            {
                last = 0;

                var cache = new Dictionary<int, Dictionary<int, bool?>>(seats.Length);

                for (var i = 0; i < seats.Length; i++)
                {
                    var row = seats[i];

                    cache.Add(i, new Dictionary<int, bool?>(row.Length));

                    for (var j = 0; j < row.Length; j++)
                    {
                        var col = row[j];

                        cache[i].Add(j, col == '#' ? true : col == 'L' ? false : (bool?)null);
                    }
                }

                for (var i = 0; i < seats.Length; i++)
                {
                    var row = seats[i].ToCharArray();
                    for (var j = 0; j < row.Length; j++)
                    {
                        var col = row[j];
                        if (col == '.')
                            continue;

                        var num = Visible(cache, i, j);

                        if (col == 'L' && num == 0)
                        {
                            row[j] = '#';

                            ++last;
                        }
                        else if (col == '#' && num >= 5)
                        {
                            row[j] = 'L';

                            ++last;
                        }
                    }

                    seats[i] = new string(row);
                }
            }
            while (last > 0);

            var occupied = seats.Sum(x => x.Count(y => y == '#'));

            return occupied;
        }

        private static int Visible(Dictionary<int, Dictionary<int, bool?>> cache, int i, int j)
        {
            var num = 0;

            var rows = cache.Count - 1;
            var cols = cache[i].Count - 1;

            int ii = i - 1;
            int jj = j - 1;
            while (ii >= 0 && jj >= 0)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                ii -= 1;
                jj -= 1;
            }

            ii = i - 1;
            jj = j;
            while (ii >= 0)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                ii -= 1;
            }

            ii = i - 1;
            jj = j + 1;
            while (ii >= 0 && jj <= cols)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                ii -= 1;
                jj += 1;
            }

            ii = i;
            jj = j - 1;
            while (jj >= 0)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                jj -= 1;
            }

            ii = i;
            jj = j + 1;
            while (jj <= cols)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                jj += 1;
            }

            ii = i + 1;
            jj = j - 1;
            while (ii <= rows && jj >= 0)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                ii += 1;
                jj -= 1;
            }

            ii = i + 1;
            jj = j;
            while (ii <= rows)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                ii += 1;
            }

            ii = i + 1;
            jj = j + 1;
            while (ii <= rows && jj <= cols)
            {
                var seat = cache[ii][jj];
                if (seat != null)
                {
                    if (seat.Value)
                        ++num;

                    break;
                }

                ii += 1;
                jj += 1;
            }

            return num;
        }
    }
}
