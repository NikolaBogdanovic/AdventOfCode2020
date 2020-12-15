using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day07
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle07.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var bags = new HashSet<string>();

            Colors(bags, lines.Where(x => !x.StartsWith("shiny gold bag")).ToArray(), "shiny gold bag");

            return bags.Count;
        }

        private static void Colors(HashSet<string> bags, string[] lines, string code)
        {
            foreach (var rule in lines)
            {
                if (rule.Contains(code))
                {
                    var color = rule.Substring(0, rule.IndexOf(" bag") + 4);
                    if (!bags.Contains(color))
                    {
                        bags.Add(color);

                        Colors(bags, lines, color);
                    }
                }
            }
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle07.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var bags = new Dictionary<string, Dictionary<string, int>>();

            foreach (var rule in lines)
            {
                var data = rule.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var color = data[0] + " " + data[1] + " bag";

                var subs = new Dictionary<string, int>();
                for (var i = 7; i < data.Length; i += 4)
                    subs.Add(data[i - 2] + " " + data[i - 1] + " bag", int.Parse(data[i - 3]));

                bags.Add(color, subs);
            }

            var num = Colors(bags, "shiny gold bag");

            return num;
        }

        private static int Colors(Dictionary<string, Dictionary<string, int>> bags, string code)
        {
            var num = 0;

            foreach (var rule in bags[code])
            {
                num += rule.Value;

                var subs = Colors(bags, rule.Key);

                num += rule.Value * subs;
            }

            return num;
        }
    }
}
