using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day16
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle16.txt", Encoding.UTF8);

            var valid = new HashSet<int>();
            var invalid = new List<int>();

            bool your = false;
            bool nearby = false;

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (!your)
                {
                    if (data.Trim() == "your ticket:")
                    {
                        your = true;
                        continue;
                    }

                    var rules = data.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var range in rules.Skip(1))
                    {
                        if (!range.Contains('-'))
                            continue;

                        var nums = range.Split('-');
                        var min = int.Parse(nums[0]);
                        var max = int.Parse(nums[1]);

                        for (var i = min; i <= max; i++)
                        {
                            if (!valid.Contains(i))
                                valid.Add(i);
                        }
                    }
                }
                else if (!nearby)
                {
                    if (data.Trim() == "nearby tickets:")
                        nearby = true;
                }
                else
                {
                    var nums = data.Split(',');
                    foreach (var i in nums)
                    {
                        var ii = int.Parse(i);
                        if (!valid.Contains(ii))
                            invalid.Add(ii);
                    }
                }
            }

            return invalid.Sum();
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle16.txt", Encoding.UTF8);

            var my = new List<int>();
            var tickets = new List<int[]>();
            var valid = new HashSet<int>();

            var fields = new Dictionary<string, HashSet<int>>();

            bool your = false;
            bool nearby = false;

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (!your)
                {
                    if (data.Trim() == "your ticket:")
                    {
                        your = true;
                        continue;
                    }

                    var rules = data.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    var values = new HashSet<int>();

                    var name = rules[0];
                    foreach (var range in rules.Skip(1))
                    {
                        if (!range.Contains('-'))
                        {
                            if (range != "or")
                                name += " " + range;

                            continue;
                        }

                        var nums = range.Split('-');
                        var min = int.Parse(nums[0]);
                        var max = int.Parse(nums[1]);

                        for (var i = min; i <= max; i++)
                        {
                            values.Add(i);

                            if (!valid.Contains(i))
                                valid.Add(i);
                        }
                    }

                    fields.Add(name, values);
                }
                else if (!nearby)
                {
                    if (data.Trim() == "nearby tickets:")
                    {
                        nearby = true;
                        continue;
                    }

                    var nums = data.Split(',');
                    for (var i = 0; i < nums.Length; i++)
                    {
                        var ii = int.Parse(nums[i]);
                        my.Add(ii);
                    }
                }
                else
                {
                    var nums = data.Split(',');
                    var values = new List<int>(nums.Length);

                    for (var i = 0; i < nums.Length; i++)
                    {
                        var ii = int.Parse(nums[i]);
                        if (!valid.Contains(ii))
                            break;

                        values.Add(ii);
                    }

                    if (values.Count == nums.Length)
                        tickets.Add(values.ToArray());
                }
            }

            tickets.Add(my.ToArray());

            var columns = new Dictionary<string, HashSet<int>>();

            for (var i = 0; i < my.Count; i++)
            {
                var values = tickets.Select(x => x[i]).Distinct().ToArray();

                foreach (var kvp in fields)
                {
                    bool invalid = false;
                    foreach (var num in values)
                    {
                        if (!kvp.Value.Contains(num))
                        {
                            invalid = true;
                            break;
                        }
                    }

                    if (invalid)
                        continue;

                    if (!columns.ContainsKey(kvp.Key))
                        columns.Add(kvp.Key, new HashSet<int>());

                    columns[kvp.Key].Add(i);
                }
            }

            var order = new Dictionary<string, int>();
            while (order.Count != fields.Count)
            {
                foreach (var kvp in columns.ToArray())
                {
                    if (kvp.Value.Count == 1)
                    {
                        order.Add(kvp.Key, kvp.Value.First());
                        columns.Remove(kvp.Key);
                        continue;
                    }

                    foreach (var i in kvp.Value)
                    {
                        var values = columns.Where(x => x.Key != kvp.Key).Select(x => x.Value).ToArray();

                        if (values.All(x => !x.Contains(i)))
                        {
                            order.Add(kvp.Key, i);
                            columns.Remove(kvp.Key);
                            break;
                        }
                    }
                }
            }

            var indices = order.Where(x => x.Key.StartsWith("departure")).Select(x => x.Value).ToArray();
            if (indices.Length == 6)
            {
                long departure = 1;
                foreach (var i in indices)
                    departure *= my[i];

                return departure;
            }

            return -1;
        }
    }
}
