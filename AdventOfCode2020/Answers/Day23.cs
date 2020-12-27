using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Answers
{
    public static class Day23
    {
        public static string PartOne()
        {
            var input = File.ReadAllText("Inputs\\Puzzle23.txt", Encoding.UTF8);

            var cups = new List<int>(input.Length);

            int start = 0;
            foreach (var data in input.Trim())
            {
                var num = int.Parse(data.ToString());

                if (start == 0)
                    start = num;

                cups.Add(num);
            }

            var index = 1;
            for (var i = 1; i <= 100; i++)
            {
                List<int> range;

                var offset = cups.Count - index;
                if (offset == 0)
                {
                    range = cups.GetRange(0, 3);
                    cups.RemoveRange(0, 3);
                }
                else if (offset >= 3)
                {
                    range = cups.GetRange(index, 3);
                    cups.RemoveRange(index, 3);
                }
                else
                {
                    range = cups.GetRange(index, offset);
                    range.AddRange(cups.GetRange(0, 3 - offset));

                    cups.RemoveRange(index, offset);
                    cups.RemoveRange(0, 3 - offset);
                }

                var min = cups.Min();

                var next = start;
                do
                {
                    --next;
                    if (next < min)
                    {
                        var max = cups.Max();
                        index = cups.IndexOf(max) + 1;
                    }
                    else
                        index = cups.IndexOf(next) + 1;
                }
                while (index == 0);

                cups.InsertRange(index, range);

                index = cups.IndexOf(start) + 1;

                if (index == cups.Count)
                {
                    start = cups[0];
                    index = 1;
                }
                else
                {
                    start = cups[index];
                    ++index;
                }
            }

            var one = cups.IndexOf(1);
            if (one > 0 && one < cups.Count - 1)
            {
                cups.RemoveAt(one);

                var range = cups.GetRange(0, one);
                cups.RemoveRange(0, one);
                cups.AddRange(range);
            }
            else if (one != -1)
                cups.RemoveAt(one);

            return string.Join("", cups);
        }

        public static long PartTwo()
        {
            var input = File.ReadAllText("Inputs\\Puzzle23.txt", Encoding.UTF8);

            var cups = new LinkedList<int>();
            var index = new List<LinkedListNode<int>>(1000000);
            index.Add(null);

            LinkedListNode<int> start = null;
            foreach (var data in input.Trim())
            {
                var num = int.Parse(data.ToString());

                LinkedListNode<int> node;
                if (start == null)
                {
                    node = cups.AddFirst(num);
                    start = node;
                }
                else
                    node = cups.AddLast(num);

                index.Add(node);
            }

            foreach (var node in index.Skip(1).ToArray())
                index[node.Value] = node;

            for (var i = cups.Max() + 1; i <= 1000000; i++)
            {
                var node = cups.AddLast(i);
                index.Add(node);
            }

            var minimal = new[] { 1, 2, 3, 4 };
            var maximal = new[] { 1000000, 999999, 999998, 999997 };

            for (var i = 1; i <= 10000000; i++)
            {
                LinkedListNode<int> first = start.Next ?? cups.First;
                LinkedListNode<int> second = first.Next ?? cups.First;
                LinkedListNode<int> third = second.Next ?? cups.First;

                cups.Remove(first);
                cups.Remove(second);
                cups.Remove(third);

                var range = new[] { first.Value, second.Value, third.Value };
                var min = minimal.Where(x => !range.Contains(x)).Min();

                var next = start.Value;
                LinkedListNode<int> node = null;
                do
                {
                    --next;
                    if (range.Contains(next))
                        continue;

                    if (next < min)
                    {
                        var max = maximal.Where(x => !range.Contains(x)).Max();
                        node = index[max];
                    }
                    else
                        node = index[next];
                }
                while (node == null);

                cups.AddAfter(node, first);
                cups.AddAfter(first, second);
                cups.AddAfter(second, third);

                start = start.Next ?? cups.First;
            }

            start = index[1];
            var one = start.Next ?? cups.First;
            var two = one.Next ?? cups.First;

            long label1 = one.Value;
            long label2 = two.Value;

            return label1 * label2;
        }
    }
}
