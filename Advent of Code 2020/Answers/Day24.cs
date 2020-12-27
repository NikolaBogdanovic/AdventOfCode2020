using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day24
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle24.txt", Encoding.UTF8);

            var tiles = new Dictionary<decimal, Dictionary<decimal, bool>>();
            tiles.Add(0, new Dictionary<decimal, bool>());
            tiles[0].Add(0, false);

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var row = 0m;
                var col = 0m;

                var half = false;
                foreach (var dir in data)
                {
                    switch (dir)
                    {
                        case 'e':
                        {
                            if (half)
                            {
                                half = false;
                                col += 0.5m;
                            }
                            else
                                ++col;

                            break;
                        }
                        case 's':
                        {
                            half = true;

                            --row;
                            break;
                        }
                        case 'w':
                        {
                            if (half)
                            {
                                half = false;
                                col -= 0.5m;
                            }
                            else
                                --col;

                            break;
                        }
                        case 'n':
                        {
                            half = true;

                            ++row;
                            break;
                        }
                    }
                }

                if (!tiles.ContainsKey(row))
                    tiles.Add(row, new Dictionary<decimal, bool>());

                if (!tiles[row].ContainsKey(col))
                    tiles[row].Add(col, true);
                else
                    tiles[row][col] = !tiles[row][col];
            }

            return tiles.Values.Sum(x => x.Values.Count(y => y));
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle24.txt", Encoding.UTF8);

            var tiles = new Dictionary<decimal, Dictionary<decimal, bool>>();
            tiles.Add(0, new Dictionary<decimal, bool>());
            tiles[0].Add(0, false);

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var row = 0m;
                var col = 0m;

                var half = false;
                foreach (var dir in data)
                {
                    switch (dir)
                    {
                        case 'e':
                        {
                            if (half)
                            {
                                half = false;
                                col += 0.5m;
                            }
                            else
                                ++col;

                            break;
                        }
                        case 's':
                        {
                            half = true;

                            --row;
                            break;
                        }
                        case 'w':
                        {
                            if (half)
                            {
                                half = false;
                                col -= 0.5m;
                            }
                            else
                                --col;

                            break;
                        }
                        case 'n':
                        {
                            half = true;

                            ++row;
                            break;
                        }
                    }
                }

                if (!tiles.ContainsKey(row))
                    tiles.Add(row, new Dictionary<decimal, bool>());

                if (!tiles[row].ContainsKey(col))
                    tiles[row].Add(col, true);
                else
                    tiles[row][col] = !tiles[row][col];
            }

            for (int i = 0; i < 100; i++)
            {
                var state = tiles.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => y.Value));
                foreach (var row in state)
                {
                    foreach (var col in row.Value)
                        AddAdjecent(tiles, row.Key, col.Key);
                }

                state = tiles.ToDictionary(x => x.Key, x => x.Value.ToDictionary(y => y.Key, y => y.Value));
                foreach (var row in state)
                {
                    foreach (var col in row.Value)
                    {
                        var num = GetFlipped(state, row.Key, col.Key);

                        if (col.Value)
                        {
                            if (num == 0 || num > 2)
                                tiles[row.Key][col.Key] = false;
                        }
                        else
                        {
                            if (num == 2)
                                tiles[row.Key][col.Key] = true;
                        }
                    }
                }
            }

            return tiles.Values.Sum(x => x.Values.Count(y => y));
        }

        private static void AddAdjecent(Dictionary<decimal, Dictionary<decimal, bool>> tiles, decimal row, decimal col)
        {
            if (!tiles.ContainsKey(row - 1))
                tiles.Add(row - 1, new Dictionary<decimal, bool>());

            if (!tiles[row - 1].ContainsKey(col - 0.5m))
                tiles[row - 1].Add(col - 0.5m, false);

            if (!tiles[row - 1].ContainsKey(col + 0.5m))
                tiles[row - 1].Add(col + 0.5m, false);

            if (!tiles[row].ContainsKey(col - 1))
                tiles[row].Add(col - 1, false);

            if (!tiles[row].ContainsKey(col + 1))
                tiles[row].Add(col + 1, false);

            if (!tiles.ContainsKey(row + 1))
                tiles.Add(row + 1, new Dictionary<decimal, bool>());

            if (!tiles[row + 1].ContainsKey(col - 0.5m))
                tiles[row + 1].Add(col - 0.5m, false);

            if (!tiles[row + 1].ContainsKey(col + 0.5m))
                tiles[row + 1].Add(col + 0.5m, false);
        }

        private static int GetFlipped(Dictionary<decimal, Dictionary<decimal, bool>> state, decimal row, decimal col)
        {
            var num = 0;

            if (state.ContainsKey(row - 1))
            {
                if (state[row - 1].ContainsKey(col - 0.5m) && state[row - 1][col - 0.5m])
                    ++num;

                if (state[row - 1].ContainsKey(col + 0.5m) && state[row - 1][col + 0.5m])
                    ++num;
            }

            if (state[row].ContainsKey(col - 1) && state[row][col - 1])
                ++num;

            if (state[row].ContainsKey(col + 1) && state[row][col + 1])
                ++num;

            if (state.ContainsKey(row + 1))
            {
                if (state[row + 1].ContainsKey(col - 0.5m) && state[row + 1][col - 0.5m])
                    ++num;

                if (state[row + 1].ContainsKey(col + 0.5m) && state[row + 1][col + 0.5m])
                    ++num;
            }

            return num;
        }
    }
}
