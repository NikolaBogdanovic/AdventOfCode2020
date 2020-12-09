using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day06
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle06.txt", Encoding.UTF8);

            var answers = new HashSet<char>(26);
            var groups = new Dictionary<int, int>(lines.Length / 2);

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    groups.Add(groups.Count, answers.Count);
                    answers.Clear();
                    continue;
                }

                foreach (var letter in data)
                {
                    if (!answers.Contains(letter))
                    {
                        answers.Add(letter);
                    }
                }
            }

            groups.Add(groups.Count, answers.Count);

            return groups.Values.Sum();
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle06.txt", Encoding.UTF8);

            var answers = new List<char>(0);
            var groups = new Dictionary<int, int>(lines.Length / 2);

            bool first = true;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    if (!first)
                    {
                        groups.Add(groups.Count, answers.Count);
                    }

                    first = true;
                    continue;
                }

                if (first)
                {
                    answers = new List<char>("abcdefghijklmnopqrstuvwxyz");
                    first = false;
                }

                for (var i = answers.Count - 1; i > -1; --i)
                {
                    var letter = answers[i];
                    if (!data.Contains(letter))
                    {
                        answers.RemoveAt(i);
                    }
                }
            }

            groups.Add(groups.Count, answers.Count);

            return groups.Values.Sum();
        }
    }
}
