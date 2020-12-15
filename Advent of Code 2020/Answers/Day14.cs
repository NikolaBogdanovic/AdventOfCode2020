using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day14
    {
        public static long PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle14.txt", Encoding.UTF8);

            var memory = new Dictionary<long, long>(lines.Length);

            string mask = null;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var kvp = data.Split(" =".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (kvp[0].StartsWith("mask"))
                {
                    mask = kvp[1].ToUpperInvariant();

                    if (mask.Any(x => x != 'X'))
                        mask = mask.PadLeft(36, 'X');
                    else
                        mask = null;

                    continue;
                }

                var mem = kvp[0].Substring(4, kvp[0].Length - 5);
                var key = long.Parse(mem);

                var dec = long.Parse(kvp[1]);
                if (mask != null)
                {
                    var bin = Convert.ToString(dec, 2);
                    bin = bin.PadLeft(36, '0');

                    var value = bin.ToCharArray();
                    for (var i = 0; i < value.Length; i++)
                    {
                        var bit = mask[i];
                        if (bit != 'X')
                            value[i] = bit;
                    }

                    bin = new string(value);
                    dec = Convert.ToInt64(bin, 2);
                }

                if (!memory.ContainsKey(key))
                    memory.Add(key, dec);
                else
                    memory[key] = dec;
            }

            return memory.Values.Sum();
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle14.txt", Encoding.UTF8);

            var memory = new Dictionary<long, long>(lines.Length);

            string mask = null;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var kvp = data.Split(" =".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (kvp[0].StartsWith("mask"))
                {
                    mask = kvp[1].ToUpperInvariant();

                    if (mask.Any(x => x != '0'))
                        mask = mask.PadLeft(36, '0');
                    else
                        mask = null;

                    continue;
                }

                var mem = kvp[0].Substring(4, kvp[0].Length - 5);

                var keys = new List<long>();
                if (mask != null)
                {
                    var bin = Convert.ToString(long.Parse(mem), 2);
                    bin = bin.PadLeft(36, '0');

                    var addresses = new List<char[]>();

                    var decoder = bin.ToCharArray();
                    for (var i = 0; i < decoder.Length; i++)
                    {
                        var bit = mask[i];
                        if (bit == '0')
                            continue;

                        if (addresses.Count == 0)
                            addresses.Add(decoder.ToArray());

                        if (bit == '1')
                        {
                            foreach (var value in addresses.ToArray())
                                value[i] = '1';

                            continue;
                        }

                        foreach (var value in addresses.ToArray())
                        {
                            var floating = value.ToArray();

                            if (floating[i] == '0')
                                floating[i] = '1';
                            else
                                floating[i] = '0';

                            addresses.Add(floating);
                        }
                    }

                    foreach (var value in addresses)
                        keys.Add(Convert.ToInt64(new string(value), 2));
                }
                else
                    keys.Add(long.Parse(mem));

                var dec = long.Parse(kvp[1]);

                foreach (var i in keys)
                {
                    if (!memory.ContainsKey(i))
                        memory.Add(i, dec);
                    else
                        memory[i] = dec;
                }
            }

            return memory.Values.Sum();
        }
    }
}
