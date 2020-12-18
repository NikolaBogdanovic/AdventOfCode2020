using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day15
    {
        public static int PartOne()
        {
            var data = File.ReadAllText("Inputs\\Puzzle15.txt", Encoding.UTF8);

            var memory = new Dictionary<int, int>();

            var index = 0;
            foreach (var i in data.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                memory.Add(int.Parse(i), ++index);

            var last = 0;
            for (var i = ++index; i <= 2020; i++)
            {
                if (!memory.ContainsKey(last))
                {
                    memory.Add(last, i);
                    last = 0;
                    continue;
                }

                var turn = memory[last];
                memory[last] = i;
                last = i - turn;
            }

            return memory.First(x => x.Value == 2020).Key;
        }

        public static int PartTwo()
        {
            var data = File.ReadAllText("Inputs\\Puzzle15.txt", Encoding.UTF8);

            var memory = new Dictionary<int, int>();

            var index = 0;
            foreach (var i in data.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                memory.Add(int.Parse(i), ++index);

            var last = 0;
            for (var i = ++index; i <= 30000000; i++)
            {
                if (!memory.ContainsKey(last))
                {
                    memory.Add(last, i);
                    last = 0;
                    continue;
                }

                var turn = memory[last];
                memory[last] = i;
                last = i - turn;
            }

            return memory.First(x => x.Value == 30000000).Key;
        }
    }
}
