using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day18
    {
        public static long PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle18.txt", Encoding.UTF8);

            long sum = 0;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                string math = data;
                while (math.Contains("("))
                    math = Parentheses(math, false);

                var num = Evaluate(math);

                sum += num;
            }

            return sum;
        }

        private static string Parentheses(string math, bool advanced)
        {
            var open = math.LastIndexOf('(');
            var close = math.IndexOf(')', open + 1);
            var exp = math.Substring(open + 1, (close - open) - 1);

            var num = advanced ? Advanced(exp) : Evaluate(exp);
            math = math.Replace('(' + exp + ')', num.ToString());

            return math;
        }

        private static long Evaluate(string exp)
        {
            long sum = 0;

            bool add = true;
            foreach (var e in exp.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                if (e == "+")
                {
                    add = true;
                    continue;
                }

                if (e == "*")
                {
                    add = false;
                    continue;
                }

                var num = long.Parse(e);

                sum = add ? sum + num : sum * num;
            }

            return sum;
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle18.txt", Encoding.UTF8);

            long sum = 0;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                string math = data;
                while (math.Contains("("))
                    math = Parentheses(math, true);

                var num = Advanced(math);

                sum += num;
            }

            return sum;
        }

        private static long Advanced(string exp)
        {
            var math = exp.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            for (var i = math.Count - 1; i > -1; i--)
            {
                if (math[i] != "+")
                    continue;

                var num = Evaluate(math[i - 1] + " + " + math[i + 1]);
                math.RemoveRange(i - 1, 3);
                math.Insert(i - 1, num.ToString());
            }

            exp = string.Join(" ", math);

            return Evaluate(exp);
        }
    }
}
