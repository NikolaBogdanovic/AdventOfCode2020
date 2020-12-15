using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day01
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle01.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            foreach (var i in lines)
            {
                var ii = int.Parse(i);
                foreach (var j in lines)
                {
                    var jj = int.Parse(j);

                    if (ii + jj == 2020)
                        return ii * jj;
                }
            }

            return -1;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle01.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            foreach (var i in lines)
            {
                var ii = int.Parse(i);
                foreach (var j in lines)
                {
                    var jj = int.Parse(j);
                    foreach (var k in lines)
                    {
                        var kk = int.Parse(k);

                        if (ii + jj + kk == 2020)
                            return ii * jj * kk;
                    }
                }
            }

            return -1;
        }
    }
}
