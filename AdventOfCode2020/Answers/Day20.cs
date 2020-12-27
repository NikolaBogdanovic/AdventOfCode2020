using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Answers
{
    public static class Day20
    {
        public static long PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle20.txt", Encoding.UTF8);

            var input = new Dictionary<int, List<string>>();

            var values = new List<string>();
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (data.StartsWith("Tile "))
                {
                    var key = data.Substring(5, data.Length - 6);
                    values = new List<string>();
                    input.Add(int.Parse(key), values);
                    continue;
                }

                values.Add(data);
            }

            var tiles = new List<Tile>(input.Count * 8);

            foreach (var kvp in input)
            {
                var item1 = kvp.Value.ToArray();
                var item2 = item1.Select(x => new string(x.Reverse().ToArray())).ToArray();
                var item3 = item1.Reverse().ToArray();
                var item4 = item3.Select(x => new string(x.Reverse().ToArray())).ToArray();

                tiles.Add(GetTile(kvp.Key, item1));
                tiles.Add(GetTile(kvp.Key, item2));
                tiles.Add(GetTile(kvp.Key, item3));
                tiles.Add(GetTile(kvp.Key, item4));

                var item5 = new string[kvp.Value.Count];
                for (var i = 0; i < item5.Length; i++)
                {
                    var value = kvp.Value.Select(x => x[i]).ToArray();
                    item5[i] = new string(value);
                }

                var item6 = item5.Select(x => new string(x.Reverse().ToArray())).ToArray();
                var item7 = item5.Reverse().ToArray();
                var item8 = item7.Select(x => new string(x.Reverse().ToArray())).ToArray();

                tiles.Add(GetTile(kvp.Key, item5));
                tiles.Add(GetTile(kvp.Key, item6));
                tiles.Add(GetTile(kvp.Key, item7));
                tiles.Add(GetTile(kvp.Key, item8));
            }

            var matches = new Dictionary<Tile, int>(tiles.Count);

            foreach (var item in tiles)
            {
                var borders = tiles.Where(x => x.Id != item.Id).ToArray();

                var left = borders.Where(x => x.Right == item.Left).Select(x => x.Id).Distinct().ToArray();
                var top = borders.Where(x => x.Bottom == item.Top).Select(x => x.Id).Distinct().ToArray();
                var right = borders.Where(x => x.Left == item.Right).Select(x => x.Id).Distinct().ToArray();
                var bottom = borders.Where(x => x.Top == item.Bottom).Select(x => x.Id).Distinct().ToArray();

                var edges = 0;

                if (left.Length > 0)
                    ++edges;

                if (top.Length > 0)
                    ++edges;

                if (right.Length > 0)
                    ++edges;

                if (bottom.Length > 0)
                    ++edges;

                matches.Add(item, edges);
            }

            var corners = matches.Where(x => x.Value == 2).Select(x => x.Key.Id).Distinct().ToArray();

            long num = 1;
            foreach (var id in corners)
                num *= id;

            return num;
        }

        private static Tile GetTile(int id, string[] data)
        {
            var borders = new Tile
            {
                Id = id,
                Top = data[0],
                Bottom = data[data.Length - 1],
                Data = data
            };

            var value = data.Select(x => x[0]).ToArray();
            borders.Left = new string(value);

            value = data.Select(x => x[x.Length - 1]).ToArray();
            borders.Right = new string(value);

            return borders;
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle20.txt", Encoding.UTF8);

            var input = new Dictionary<int, List<string>>();

            var values = new List<string>();
            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (data.StartsWith("Tile "))
                {
                    var key = data.Substring(5, data.Length - 6);
                    values = new List<string>();
                    input.Add(int.Parse(key), values);
                    continue;
                }

                values.Add(data);
            }

            var tiles = new List<Tile>(input.Count * 8);

            foreach (var kvp in input)
            {
                var item1 = kvp.Value.ToArray();
                var item2 = item1.Select(x => new string(x.Reverse().ToArray())).ToArray();
                var item3 = item1.Reverse().ToArray();
                var item4 = item3.Select(x => new string(x.Reverse().ToArray())).ToArray();

                tiles.Add(GetTile(kvp.Key, item1));
                tiles.Add(GetTile(kvp.Key, item2));
                tiles.Add(GetTile(kvp.Key, item3));
                tiles.Add(GetTile(kvp.Key, item4));

                var item5 = new string[kvp.Value.Count];
                for (var i = 0; i < item5.Length; i++)
                {
                    var value = kvp.Value.Select(x => x[i]).ToArray();
                    item5[i] = new string(value);
                }

                var item6 = item5.Select(x => new string(x.Reverse().ToArray())).ToArray();
                var item7 = item5.Reverse().ToArray();
                var item8 = item7.Select(x => new string(x.Reverse().ToArray())).ToArray();

                tiles.Add(GetTile(kvp.Key, item5));
                tiles.Add(GetTile(kvp.Key, item6));
                tiles.Add(GetTile(kvp.Key, item7));
                tiles.Add(GetTile(kvp.Key, item8));
            }

            var matches = new Dictionary<Tile, int>(tiles.Count);

            foreach (var item in tiles)
            {
                var borders = tiles.Where(x => x.Id != item.Id).ToArray();

                var left = borders.Where(x => x.Right == item.Left).Select(x => x.Id).Distinct().ToArray();
                var top = borders.Where(x => x.Bottom == item.Top).Select(x => x.Id).Distinct().ToArray();
                var right = borders.Where(x => x.Left == item.Right).Select(x => x.Id).Distinct().ToArray();
                var bottom = borders.Where(x => x.Top == item.Bottom).Select(x => x.Id).Distinct().ToArray();

                var edges = 0;

                if (left.Length > 0)
                    ++edges;

                if (top.Length > 0)
                    ++edges;

                if (right.Length > 0)
                    ++edges;

                if (bottom.Length > 0)
                    ++edges;

                matches.Add(item, edges);
            }

            var side = (int)Math.Sqrt(input.Count);
            var camera = new Dictionary<int, Dictionary<int, Tile>>(side);

            for (var i = 0; i < side; i++)
            {
                camera.Add(i, new Dictionary<int, Tile>(side));

                for (var j = 0; j < side; j++)
                    camera[i].Add(j, null);
            }

            var ids = new HashSet<int>();
            foreach (var item in matches.Where(x => x.Value == 2).Select(x => x.Key))
            {
                bool error = false;

                for (var i = 0; i < side; i++)
                {
                    for (var j = 0; j < side; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            camera[0][0] = item;
                            ids.Add(item.Id);
                            continue;
                        }

                        int edges;
                        if (i == 0)
                        {
                            if (j == side - 1)
                                edges = 2;
                            else
                                edges = 3;
                        }
                        else if (i == side - 1)
                        {
                            if (j == 0 || j == side - 1)
                                edges = 2;
                            else
                                edges = 3;
                        }
                        else if (j == 0 || j == side - 1)
                            edges = 3;
                        else
                            edges = 4;

                        var top = i > 0 ? camera[i - 1][j] : null;
                        var left = j > 0 ? camera[i][j - 1] : null;

                        var center = matches.Where(x => x.Value == edges && !ids.Contains(x.Key.Id) && (top == null || top.Bottom == x.Key.Top) && (left == null || left.Right == x.Key.Left)).ToArray();
                        if (center.Length != 1)
                        {
                            error = true;
                            break;
                        }

                        camera[i][j] = center[0].Key;
                        ids.Add(center[0].Key.Id);
                    }

                    if (error)
                    {
                        ids.Clear();
                        break;
                    }
                }

                if (!error)
                    break;
            }

            var cropped = new Dictionary<int, Dictionary<int, string[]>>(side);

            for (var i = 0; i < side; i++)
            {
                cropped.Add(i, new Dictionary<int, string[]>(side));

                for (var j = 0; j < side; j++)
                {
                    var data = camera[i][j].Data;

                    var copy = new string[data.Length - 2];
                    for (var k = 1; k < data.Length - 1; k++)
                    {
                        var value = data[k].Skip(1).Take(copy.Length).ToArray();
                        copy[k - 1] = new string(value);
                    }

                    cropped[i].Add(j, copy);
                }
            }

            var edge = cropped[0][0].Length;
            var image = new string[side * edge];

            for (var i = 0; i < cropped.Count; i++)
            {
                var crops = cropped[i].Values;

                for (var j = 0; j < edge; j++)
                {
                    image[(i * edge) + j] = string.Join("", crops.Select(x => x[j]).ToArray());
                }
            }

            var monster = new List<string[]>(8);

            var sea1 = image;
            var sea2 = sea1.Select(x => new string(x.Reverse().ToArray())).ToArray();
            var sea3 = sea1.Reverse().ToArray();
            var sea4 = sea3.Select(x => new string(x.Reverse().ToArray())).ToArray();

            monster.Add(sea1);
            monster.Add(sea2);
            monster.Add(sea3);
            monster.Add(sea4);

            var sea5 = new string[image.Length];
            for (var i = 0; i < sea5.Length; i++)
            {
                var value = image.Select(x => x[i]).ToArray();
                sea5[i] = new string(value);
            }

            var sea6 = sea5.Select(x => new string(x.Reverse().ToArray())).ToArray();
            var sea7 = sea5.Reverse().ToArray();
            var sea8 = sea7.Select(x => new string(x.Reverse().ToArray())).ToArray();

            monster.Add(sea5);
            monster.Add(sea6);
            monster.Add(sea7);
            monster.Add(sea8);

            var upper = new Regex("#....##....##....###", RegexOptions.Compiled);
            var lower = new Regex("#..#..#..#..#..#", RegexOptions.Compiled);

            foreach (var item in monster)
            {
                var num = 0;

                for (var i = item.Length - 2; i > 0; i--)
                {
                    var row = item[i];

                    var match = upper.Match(row);
                    while (match.Success)
                    {
                        if (item[i + 1][match.Index + 18] == '#') // head
                        {
                            var body = lower.Match(item[i - 1], match.Index + 1);
                            if (body.Success && body.Index == match.Index + 1) // align
                                ++num;
                        }

                        match = upper.Match(row, match.Index + 1);
                    }
                }

                if (num > 0)
                {
                    var rough = item.Sum(x => x.Count(y => y == '#'));
                    return rough - (num * 15);
                }
            }

            return -1;
        }

        private class Tile
        {
            public int Id { get; set; }

            public string Left { get; set; }

            public string Top { get; set; }

            public string Right { get; set; }

            public string Bottom { get; set; }

            public string[] Data { get; set; }
        }
    }
}
