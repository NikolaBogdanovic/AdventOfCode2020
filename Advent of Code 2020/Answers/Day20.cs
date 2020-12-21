using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day20
    {
        public static int PartOne()
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

            var tiles = new Dictionary<int, Borders[]>();

            foreach (var kvp in input)
            {
                var item1 = kvp.Value.ToArray();
                var item2 = item1.Reverse().ToArray();
                var item3 = item1.Select(x => new string(x.Reverse().ToArray())).ToArray();
                var item4 = item2.Select(x => new string(x.Reverse().ToArray())).ToArray();

                var borders1 = GetBorders(kvp.Key, item1);
                var borders2 = GetBorders(kvp.Key, item2);
                var borders3 = GetBorders(kvp.Key, item3);
                var borders4 = GetBorders(kvp.Key, item4);

                tiles.Add(kvp.Key, new[] { borders1, borders2, borders3, borders4 });
            }

            var image = new Dictionary<int, Dictionary<int, Borders>>((int)Math.Sqrt(tiles.Count));

            foreach (var kvp in tiles)
            {
                foreach (var item in kvp.Value)
                {
                    var borders = tiles.Where(x => x.Key != kvp.Key).SelectMany(x => x.Value);
                    if (borders.All(x => x.Bottom != item.Top && x.Right != item.Left))
                    {
                        image.Add(0, new Dictionary<int, Borders>());
                        image[0].Add(0, item);
                    }
                }
            }

            return -1;
        }

        private static Borders GetBorders(int key, string[] item1)
        {
            var borders = new Borders
            {
                Tile = key,
                Left = item1.Select(x => x[0] == '#').ToArray(),
                Top = item1[0].Select(x => x == '#').ToArray(),
                Right = item1.Select(x => x[x.Length - 1] == '#').ToArray(),
                Bottom = item1[item1.Length - 1].Select(x => x == '#').ToArray()
            };

            return borders;
        }

        private class Borders
        {
            public int Tile { get; set; }

            public bool[] Left { get; set; }

            public bool[] Top { get; set; }

            public bool[] Right { get; set; }

            public bool[] Bottom { get; set; }
        }
    }
}
