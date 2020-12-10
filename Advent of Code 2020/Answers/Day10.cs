using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day10
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle10.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var adapters = lines.Select(int.Parse).OrderBy(x => x).ToList();
            adapters.Add(adapters.Last() + 3);

            int ones = 0;
            int twos = 0;
            int threes = 0;

            var jolts = 0;
            while (adapters.Count > 0)
            {
                var num = adapters[0];
                adapters.RemoveAt(0);

                if (num == jolts + 1)
                    ++ones;
                else if (num == jolts + 2)
                    ++twos;
                else if (num == jolts + 3)
                    ++threes;

                jolts = num;
            }

            return ones * threes;
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle10.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var adapters = lines.Select(int.Parse).OrderBy(x => x).ToList();
            adapters.Add(adapters.Last() + 3);

            var num = Arrangements(new Dictionary<int, long>(adapters.Count), adapters, 0, 0);

            return num;
        }

        private static long Arrangements(Dictionary<int, long> cache, List<int> adapters, int index, int jolts)
        {
            var distinct = adapters.Skip(index).Take(3).Where(x => x <= jolts + 3).OrderBy(x => x).ToArray();
            if (distinct.Length == 0)
                return 0;

            long num = 0;

            if (jolts == 0)
                num += distinct.Length;
            else if (distinct.Length > 1)
                num += distinct.Length - 1;

            foreach (var i in distinct)
            {
                // takes forever without caching
                if (!cache.ContainsKey(i))
                    cache[i] = Arrangements(cache, adapters, ++index, i);

                num += cache[i];
            }

            return num;
        }
    }
}
