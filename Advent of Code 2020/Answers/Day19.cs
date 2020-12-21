using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day19
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle19.txt", Encoding.UTF8);

            var rules = new Dictionary<string, List<string>>(lines.Length / 2);

            var messages = new List<string>(lines.Length / 2);

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var text = data.Trim();

                if (!char.IsDigit(text[0]))
                {
                    messages.Add(text);
                    continue;
                }

                var input = text.Split(':');
                var key = input[0];

                var values = input[1].Split('|');
                for (var i = 0; i < values.Length; i++)
                {
                    var item = values[i];
                    if (item.Contains('"'))
                    {
                        values[i] = item.Trim(" \"".ToCharArray());
                        continue;
                    }

                    item = item.Trim().Replace(" ", "  ");
                    values[i] = " " + item + " ";
                }

                rules.Add(key, new List<string>(values));
            }

            var keys = rules.Keys.ToHashSet();
            while (keys.Count > 0)
            {
                foreach (var item in rules.Where(x => keys.Contains(x.Key) && x.Value.All(y => !y.Contains(" "))).ToArray())
                {
                    keys.Remove(item.Key);

                    var sub = " " + item.Key + " ";
                    foreach (var copy in rules.Where(x => keys.Contains(x.Key) && x.Value.Any(y => y.Contains(sub))).ToArray())
                    {
                        var i = -1;
                        foreach (var nums in copy.Value.ToArray())
                        {
                            if (++i == 0)
                                copy.Value.Clear();

                            if (nums.Contains(sub))
                            {
                                var list = new List<string>();

                                int index = nums.IndexOf(sub);
                                do
                                {
                                    if (list.Count == 0)
                                    {
                                        var text = nums.ToList();
                                        text.RemoveRange(index, sub.Length);

                                        foreach (var letters in item.Value)
                                        {
                                            text.InsertRange(index, letters.ToCharArray());
                                            list.Add(new string(text.ToArray()));
                                            text.RemoveRange(index, letters.Length);
                                        }
                                    }
                                    else
                                    {
                                        var j = -1;
                                        foreach (var data in list.ToArray())
                                        {
                                            if (++j == 0)
                                                list.Clear();

                                            var next = data.IndexOf(sub);
                                            var text = data.ToList();
                                            text.RemoveRange(next, sub.Length);

                                            foreach (var letters in item.Value)
                                            {
                                                text.InsertRange(next, letters.ToCharArray());
                                                list.Add(new string(text.ToArray()));
                                                text.RemoveRange(next, letters.Length);
                                            }
                                        }
                                    }

                                    index = nums.IndexOf(sub, index + sub.Length);
                                }
                                while (index != -1);

                                copy.Value.AddRange(list);
                            }
                            else
                                copy.Value.Add(nums);
                        }
                    }
                }
            }

            var rule0 = rules["0"].ToHashSet();

            var num = 0;
            foreach (var item in messages)
            {
                if (rule0.Contains(item))
                    ++num;
            }

            return num;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle19.txt", Encoding.UTF8);

            var rules = new Dictionary<string, List<string>>(lines.Length / 2);

            var messages = new List<string>(lines.Length / 2);

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                var text = data.Trim();

                if (!char.IsDigit(text[0]))
                {
                    messages.Add(text);
                    continue;
                }

                var input = text.Split(':');
                var key = input[0];

                var values = input[1].Split('|');
                for (var i = 0; i < values.Length; i++)
                {
                    var item = values[i];
                    if (item.Contains('"'))
                    {
                        values[i] = item.Trim(" \"".ToCharArray());
                        continue;
                    }

                    item = item.Trim().Replace(" ", "  ");
                    values[i] = " " + item + " ";
                }

                rules.Add(key, new List<string>(values));
            }

            var keys = rules.Keys.ToHashSet();
            while (keys.Count > 0)
            {
                foreach (var item in rules.Where(x => keys.Contains(x.Key) && x.Value.All(y => !y.Contains(" "))).ToArray())
                {
                    keys.Remove(item.Key);

                    var sub = " " + item.Key + " ";
                    foreach (var copy in rules.Where(x => keys.Contains(x.Key) && x.Value.Any(y => y.Contains(sub))).ToArray())
                    {
                        var i = -1;
                        foreach (var nums in copy.Value.ToArray())
                        {
                            if (++i == 0)
                                copy.Value.Clear();

                            if (nums.Contains(sub))
                            {
                                var list = new List<string>();

                                int index = nums.IndexOf(sub);
                                do
                                {
                                    if (list.Count == 0)
                                    {
                                        var text = nums.ToList();
                                        text.RemoveRange(index, sub.Length);

                                        foreach (var letters in item.Value)
                                        {
                                            text.InsertRange(index, letters.ToCharArray());
                                            list.Add(new string(text.ToArray()));
                                            text.RemoveRange(index, letters.Length);
                                        }
                                    }
                                    else
                                    {
                                        var j = -1;
                                        foreach (var data in list.ToArray())
                                        {
                                            if (++j == 0)
                                                list.Clear();

                                            var next = data.IndexOf(sub);
                                            var text = data.ToList();
                                            text.RemoveRange(next, sub.Length);

                                            foreach (var letters in item.Value)
                                            {
                                                text.InsertRange(next, letters.ToCharArray());
                                                list.Add(new string(text.ToArray()));
                                                text.RemoveRange(next, letters.Length);
                                            }
                                        }
                                    }

                                    index = nums.IndexOf(sub, index + sub.Length);
                                }
                                while (index != -1);

                                copy.Value.AddRange(list);
                            }
                            else
                                copy.Value.Add(nums);
                        }
                    }
                }
            }

            // 0: 8 11
            // 8: 42 | 42 8
            // 11: 42 31 | 42 11 31
            //
            // 8-1 11-1: 42 42 31
            // 8-1 11-2: 42 42...42 31...31
            // 8-2 11-1: 42...42...42 31
            // 8-2 11-2: 42...42...42...42 31...31

            var rule0 = rules["0"].ToHashSet();

            var rule42 = rules["42"].ToList();
            var rule31 = rules["31"].ToList();

            var num = 0;
            foreach (var item in messages)
            {
                if (rule0.Contains(item))
                {
                    ++num;
                    continue;
                }

                var num42 = 0;

                var i = 0;
                var j = 0;
                do
                {
                    foreach (var data in rule42)
                    {
                        j = item.IndexOf(data, i);
                        if (j == i)
                        {
                            ++num42;
                            i += data.Length;
                            j = i;
                            break;
                        }
                    }
                }
                while (j == i && i < item.Length);

                if (num42 == 0 || i == item.Length)
                    continue;

                var num31 = 0;

                do
                {
                    foreach (var data in rule31)
                    {
                        j = item.IndexOf(data, i);
                        if (j == i)
                        {
                            ++num31;
                            i += data.Length;
                            j = i;
                            break;
                        }
                    }
                }
                while (j == i && i < item.Length);

                if (num31 > 0 && num31 < num42 && i == item.Length)
                    ++num;
            }

            return num;
        }
    }
}
