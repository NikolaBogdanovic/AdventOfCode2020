using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day13
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle13.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var timestamp = int.Parse(lines[0]);

            var busIds = lines[1]
                .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Where(x => x != "x")
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToArray();

            var max = busIds.Max();

            var departures = new Dictionary<int, Dictionary<int, bool>>(max);

            for (var i = timestamp; i < timestamp + max; i++)
            {
                var departs = new Dictionary<int, bool>();

                foreach (var id in busIds)
                    departs.Add(id, i == 0 || i % id == 0);

                departures.Add(i, departs);
            }

            var next = departures.First(x => x.Key >= timestamp && x.Value.Values.Any(y => y));

            return (next.Key - timestamp) * next.Value.First(x => x.Value).Key;
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle13.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            ////var timestamp = int.Parse(lines[0]);

            var busIds = new Dictionary<int, int>();

            var index = 0;
            foreach (var id in lines[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                if (id != "x")
                    busIds.Add(index, int.Parse(id));

                ++index;
            }

            var first = busIds.First();
            busIds.Remove(first.Key);

            var min = 100000000000000;
            if (min % first.Value != 0)
                min = ((100000000000000 / first.Value) + 1) * first.Value;

            for (var i = min; i <= long.MaxValue; i += first.Value)
            {
                bool timestamp = true;

                foreach (var id in busIds)
                {
                    if ((i + id.Key) % id.Value != 0)
                    {
                        timestamp = false;
                        break;
                    }
                }

                if (timestamp)
                    return i;
            }

            return -1;
        }

        public static long PartTwoVerySlow()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle13.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            ////var timestamp = int.Parse(lines[0]);

            var busIds = new Dictionary<int, int>();

            var index = 0;
            foreach (var id in lines[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                if (id != "x")
                    busIds.Add(index, int.Parse(id));

                ++index;
            }

            var first = busIds.First();
            busIds.Remove(first.Key);

            var min = 100000000000000;
            if (min % first.Value != 0)
                min = ((100000000000000 / first.Value) + 1) * first.Value;

            for (var i = min; i <= long.MaxValue; i += first.Value)
            {
                bool timestamp = true;

                foreach (var id in busIds)
                {
                    if ((i + id.Key) % id.Value != 0)
                    {
                        timestamp = false;
                        break;
                    }
                }

                if (timestamp)
                    return i;
            }

            return -1;
        }
    }
}
