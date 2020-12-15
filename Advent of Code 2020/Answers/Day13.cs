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

            var busIds = new List<KeyValuePair<long, long>>();

            long index = 0;
            foreach (var id in lines[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                if (id != "x")
                    busIds.Add(new KeyValuePair<long, long>(index, int.Parse(id)));

                ++index;
            }

            busIds.Reverse();
            for (var i = busIds.Count - 1; i > -1; --i)
            {
                if (i == 0)
                    return busIds[0].Key;

                var prev = busIds[i];
                var next = busIds[i - 1];

                var timestamp = prev.Key;
                while ((timestamp + next.Key) % next.Value != 0)
                    timestamp += prev.Value;

                busIds.RemoveRange(i - 1, 2);
                busIds.Add(new KeyValuePair<long, long>(timestamp, prev.Value * next.Value));
            }

            return -1;
        }

        public static long PartTwoBruteForceVerySlow()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle13.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            ////var timestamp = int.Parse(lines[0]);

            var busIds = new Dictionary<int, int>();

            var max = 0;
            var index = 0;
            foreach (var id in lines[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                if (id != "x")
                {
                    busIds.Add(index, int.Parse(id));

                    if (max < busIds[index])
                        max = index;
                }

                ++index;
            }

            index = max;
            max = busIds[index];
            busIds.Remove(index);

            var min = 100000000000000;
            if (min % max != 0)
                min = ((100000000000000 / max) + 1) * max;

            for (var i = min; i <= long.MaxValue; i += max)
            {
                bool timestamp = true;

                foreach (var id in busIds)
                {
                    if ((i - (index - id.Key)) % id.Value != 0)
                    {
                        timestamp = false;
                        break;
                    }
                }

                if (timestamp)
                    return i - index;
            }

            return -1;
        }
    }
}
