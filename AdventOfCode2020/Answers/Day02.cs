using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Answers
{
    public static class Day02
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle02.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var data = lines.ToLookup(x => x.Split(':')[0], x => x.Split(':')[1].Trim());

            var num = 0;

            foreach (var group in data)
            {
                var key = group.Key.Split("- ".ToCharArray());
                var i = int.Parse(key[0]);
                var j = int.Parse(key[1]);
                var k = key[2][0];

                foreach (var l in group)
                {
                    var m = l.Count(x => x == k);
                    if (m >= i && m <= j)
                        ++num;
                }
            }

            return num;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle02.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var data = lines.ToLookup(x => x.Split(':')[0], x => x.Split(':')[1].Trim());

            var num = 0;

            foreach (var group in data)
            {
                var key = group.Key.Split("- ".ToCharArray());
                var i = int.Parse(key[0]) - 1;
                var j = int.Parse(key[1]) - 1;
                var k = key[2][0];

                foreach (var l in group)
                {
                    if (l.Length > i && l[i] == k && (l.Length < j || l[j] != k))
                        ++num;
                    else if (l.Length > i && l[i] != k && l.Length > j && l[j] == k)
                        ++num;
                }
            }

            return num;
        }
    }
}
