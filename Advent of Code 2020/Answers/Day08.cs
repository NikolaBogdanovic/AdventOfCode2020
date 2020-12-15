using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day08
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle08.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var code = new HashSet<int>();

            bool error;
            var acc = Exec(code, lines, 0, out error);

            return acc;
        }

        private static int Exec(HashSet<int> code, string[] lines, int num, out bool error)
        {
            var acc = 0;

            if (!code.Contains(num) && num >= 0 && num <= lines.Length)
            {
                if (num == lines.Length)
                {
                    error = false;
                    return acc;
                }

                code.Add(num);

                var cmd = lines[num];
                switch (cmd.Split(' ')[0])
                {
                    case "acc":
                    {
                        acc += int.Parse(cmd.Split(' ')[1]);
                        acc += Exec(code, lines, num + 1, out error);

                        break;
                    }
                    case "jmp":
                    {
                        var jmp = int.Parse(cmd.Split(' ')[1]);
                        acc += Exec(code, lines, num + jmp, out error);

                        break;
                    }
                    case "nop":
                    default:
                    {
                        acc += Exec(code, lines, num + 1, out error);
                        break;
                    }
                }
            }
            else
                error = true;

            return acc;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle08.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var copy = lines.ToArray();
            var code = new HashSet<int>();

            int acc;
            int last = 0;

            bool error;
            do
            {
                acc = Exec(code, copy, 0, out error);

                copy = lines.ToArray();
                for (int i = last; i < copy.Length; i++)
                {
                    if (copy[i].StartsWith("jmp"))
                    {
                        copy[i] = "nop" + copy[i].Substring(3);

                        last = i + 1;
                        break;
                    }

                    if (copy[i].StartsWith("nop"))
                    {
                        copy[i] = "jmp" + copy[i].Substring(3);

                        last = i + 1;
                        break;
                    }
                }

                code.Clear();
            }
            while (error);

            return acc;
        }
    }
}
