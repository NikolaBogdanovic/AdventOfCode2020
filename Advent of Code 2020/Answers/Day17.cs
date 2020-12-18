using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day17
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle17.txt", Encoding.UTF8);

            var cubes = new Dictionary<int, Dictionary<int, Dictionary<int, bool>>>(13);
            cubes.Add(0, new Dictionary<int, Dictionary<int, bool>>());

            var rowNum = 0;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                cubes[0].Add(rowNum, new Dictionary<int, bool>());

                var colNum = 0;
                foreach (var dot in data.ToCharArray())
                {
                    cubes[0][rowNum].Add(colNum, dot == '#');

                    ++colNum;
                }

                ++rowNum;
            }

            for (var i = 0; i < 6; i++)
            {
                foreach (var len in cubes)
                {
                    len.Value.Add(len.Value.Keys.Min() - 1, len.Value[0].ToDictionary(x => x.Key, x => false));
                    len.Value.Add(len.Value.Keys.Max() + 1, len.Value[0].ToDictionary(x => x.Key, x => false));

                    foreach (var row in len.Value)
                    {
                        row.Value.Add(row.Value.Keys.Min() - 1, false);
                        row.Value.Add(row.Value.Keys.Max() + 1, false);
                    }
                }

                cubes.Add(-1 - i, cubes[0].ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => false)));
                cubes.Add(1 + i, cubes[0].ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => false)));

                var state = cubes.ToDictionary(z => z.Key, z => z.Value.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => y.Value)));

                foreach (var len in state)
                {
                    foreach (var row in len.Value)
                    {
                        foreach (var col in row.Value)
                        {
                            var num = ActiveNeighbors(state, len.Key, row.Key, col.Key);

                            if (col.Value)
                            {
                                if (num != 2 && num != 3)
                                    cubes[len.Key][row.Key][col.Key] = false;
                            }
                            else
                            {
                                if (num == 3)
                                    cubes[len.Key][row.Key][col.Key] = true;
                            }
                        }
                    }
                }
            }

            var active = 0;
            foreach (var len in cubes)
            {
                foreach (var row in len.Value)
                {
                    foreach (var col in row.Value)
                    {
                        if (col.Value)
                            ++active;
                    }
                }
            }

            return active;
        }

        private static int ActiveNeighbors(Dictionary<int, Dictionary<int, Dictionary<int, bool>>> state, int lenNum, int rowNum, int colNum)
        {
            var num = 0;

            for (var z = lenNum - 1; z <= lenNum + 1; z++)
            {
                for (var x = rowNum - 1; x <= rowNum + 1; x++)
                {
                    for (var y = colNum - 1; y <= colNum + 1; y++)
                    {
                        if (z != lenNum || x != rowNum || y != colNum)
                        {
                            if (state.ContainsKey(z) && state[z].ContainsKey(x) && state[z][x].ContainsKey(y))
                            {
                                if (state[z][x][y])
                                    ++num;
                            }
                        }
                    }
                }
            }

            return num;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle17.txt", Encoding.UTF8);

            var cubes = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, bool>>>>(13);
            cubes.Add(0, new Dictionary<int, Dictionary<int, Dictionary<int, bool>>>(13));
            cubes[0].Add(0, new Dictionary<int, Dictionary<int, bool>>());

            var rowNum = 0;
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                cubes[0][0].Add(rowNum, new Dictionary<int, bool>());

                var colNum = 0;
                foreach (var dot in data.ToCharArray())
                {
                    cubes[0][0][rowNum].Add(colNum, dot == '#');

                    ++colNum;
                }

                ++rowNum;
            }

            for (var i = 0; i < 6; i++)
            {
                foreach (var len in cubes[0])
                {
                    len.Value.Add(len.Value.Keys.Min() - 1, len.Value[0].ToDictionary(x => x.Key, x => false));
                    len.Value.Add(len.Value.Keys.Max() + 1, len.Value[0].ToDictionary(x => x.Key, x => false));

                    foreach (var row in len.Value)
                    {
                        row.Value.Add(row.Value.Keys.Min() - 1, false);
                        row.Value.Add(row.Value.Keys.Max() + 1, false);
                    }
                }

                cubes[0].Add(-1 - i, cubes[0][0].ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => false)));
                cubes[0].Add(1 + i, cubes[0][0].ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => false)));

                cubes.Add(-1 - i, cubes[0].ToDictionary(z => z.Key, z => z.Value.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => false))));
                cubes.Add(1 + i, cubes[0].ToDictionary(z => z.Key, z => z.Value.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => false))));

                var state = cubes.ToDictionary(w => w.Key, w => w.Value.ToDictionary(z => z.Key, z => z.Value.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => y.Value))));

                foreach (var hyp in state)
                {
                    foreach (var len in hyp.Value)
                    {
                        foreach (var row in len.Value)
                        {
                            foreach (var col in row.Value)
                            {
                                var num = ActiveNeighbors(state, hyp.Key, len.Key, row.Key, col.Key);

                                if (col.Value)
                                {
                                    if (num != 2 && num != 3)
                                        cubes[hyp.Key][len.Key][row.Key][col.Key] = false;
                                }
                                else
                                {
                                    if (num == 3)
                                        cubes[hyp.Key][len.Key][row.Key][col.Key] = true;
                                }
                            }
                        }
                    }
                }
            }

            var active = 0;
            foreach (var hyp in cubes)
            {
                foreach (var len in hyp.Value)
                {
                    foreach (var row in len.Value)
                    {
                        foreach (var col in row.Value)
                        {
                            if (col.Value)
                                ++active;
                        }
                    }
                }
            }

            return active;
        }

        private static int ActiveNeighbors(Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, bool>>>> state, int hypNum, int lenNum, int rowNum, int colNum)
        {
            var num = 0;

            for (var w = hypNum - 1; w <= hypNum + 1; w++)
            {
                for (var z = lenNum - 1; z <= lenNum + 1; z++)
                {
                    for (var x = rowNum - 1; x <= rowNum + 1; x++)
                    {
                        for (var y = colNum - 1; y <= colNum + 1; y++)
                        {
                            if (w != hypNum || z != lenNum || x != rowNum || y != colNum)
                            {
                                if (state.ContainsKey(w) && state[w].ContainsKey(z) && state[w][z].ContainsKey(x) && state[w][z][x].ContainsKey(y))
                                {
                                    if (state[w][z][x][y])
                                        ++num;
                                }
                            }
                        }
                    }
                }
            }

            return num;
        }
    }
}
