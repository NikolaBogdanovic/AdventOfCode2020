using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day21
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle21.txt", Encoding.UTF8);

            var alergens = new Dictionary<string, Dictionary<string, int>>();

            var ingredients = new List<string>();

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var list = data.Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var left = list[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var right = list[1].Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                ingredients.AddRange(left);

                foreach (var key in right.Skip(1))
                {
                    if (!alergens.ContainsKey(key))
                        alergens.Add(key, new Dictionary<string, int>());

                    var values = alergens[key];
                    foreach (var item in left)
                    {
                        if (!values.ContainsKey(item))
                            values.Add(item, 0);

                        values[item] += 1;
                    }
                }
            }

            foreach (var item in alergens.Where(x => x.Value.Count == 1).Select(x => x.Key).ToArray())
                RemoveCopies(alergens, item);

            while (alergens.Values.Any(x => x.Count > 1))
            {
                foreach (var kvp in alergens.Where(x => x.Value.Count > 1))
                {
                    var max = kvp.Value.Values.Max(x => x);
                    var list = kvp.Value.Where(x => x.Value == max).Select(x => x.Key).ToArray();
                    if (list.Length == 1)
                    {
                        var item = list[0];

                        kvp.Value.Clear();
                        kvp.Value.Add(item, max);

                        RemoveCopies(alergens, item);
                    }
                }
            }

            var dangerous = alergens.Values.Select(x => x.First().Key).ToArray();

            var num = 0;
            foreach (var item in ingredients)
            {
                if (!dangerous.Contains(item))
                    ++num;
            }

            return num;
        }

        public static void RemoveCopies(Dictionary<string, Dictionary<string, int>> list, string item)
        {
            foreach (var copy in list.Where(x => x.Value.Count > 1 && x.Value.ContainsKey(item)).ToArray())
            {
                copy.Value.Remove(item);

                if (copy.Value.Count == 1)
                    RemoveCopies(list, copy.Value.First().Key);
            }
        }

        public static string PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle21.txt", Encoding.UTF8);

            var alergens = new Dictionary<string, Dictionary<string, int>>();

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var list = data.Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var left = list[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var right = list[1].Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var key in right.Skip(1))
                {
                    if (!alergens.ContainsKey(key))
                        alergens.Add(key, new Dictionary<string, int>());

                    var values = alergens[key];
                    foreach (var item in left)
                    {
                        if (!values.ContainsKey(item))
                            values.Add(item, 0);

                        values[item] += 1;
                    }
                }
            }

            foreach (var item in alergens.Where(x => x.Value.Count == 1).Select(x => x.Key).ToArray())
                RemoveCopies(alergens, item);

            while (alergens.Values.Any(x => x.Count > 1))
            {
                foreach (var kvp in alergens.Where(x => x.Value.Count > 1))
                {
                    var max = kvp.Value.Values.Max(x => x);
                    var list = kvp.Value.Where(x => x.Value == max).Select(x => x.Key).ToArray();
                    if (list.Length == 1)
                    {
                        var item = list[0];

                        kvp.Value.Clear();
                        kvp.Value.Add(item, max);

                        RemoveCopies(alergens, item);
                    }
                }
            }

            var dangerous = alergens.OrderBy(x => x.Key).Select(x => x.Value.First().Key).ToArray();

            return string.Join(",", dangerous);
        }
    }
}
