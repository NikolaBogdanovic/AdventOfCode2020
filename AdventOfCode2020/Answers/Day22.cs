using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Answers
{
    public static class Day22
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle22.txt", Encoding.UTF8);

            Queue<int> player1 = null;
            Queue<int> player2 = null;

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (data.Trim() == "Player 1:")
                {
                    player1 = new Queue<int>();
                    continue;
                }

                if (data.Trim() == "Player 2:")
                {
                    player2 = new Queue<int>();
                    continue;
                }

                if (player2 != null)
                    player2.Enqueue(int.Parse(data));
                else if (player1 != null)
                    player1.Enqueue(int.Parse(data));
            }

            while (player1.Count > 0 && player2.Count > 0)
            {
                var num1 = player1.Dequeue();
                var num2 = player2.Dequeue();

                if (num1 > num2)
                {
                    player1.Enqueue(num1);
                    player1.Enqueue(num2);
                }
                else
                {
                    player2.Enqueue(num2);
                    player2.Enqueue(num1);
                }
            }

            var winner = player1.Count > 0 ? player1 : player2;

            var sum = 0;

            var index = winner.Count;
            while (winner.Count > 0)
            {
                var num = winner.Dequeue();
                sum += num * index--;
            }

            return sum;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle22.txt", Encoding.UTF8);

            Queue<int> player1 = null;
            Queue<int> player2 = null;

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (data.Trim() == "Player 1:")
                {
                    player1 = new Queue<int>();
                    continue;
                }

                if (data.Trim() == "Player 2:")
                {
                    player2 = new Queue<int>();
                    continue;
                }

                if (player2 != null)
                    player2.Enqueue(int.Parse(data));
                else if (player1 != null)
                    player1.Enqueue(int.Parse(data));
            }

            // takes forever without caching
            var cache = new Dictionary<string, int>();

            var win = RecursiveCombat(cache, player1, player2);

            var winner = win == 1 ? player1 : player2;

            var sum = 0;

            var index = winner.Count;
            while (winner.Count > 0)
            {
                var num = winner.Dequeue();
                sum += num * index--;
            }

            return sum;
        }

        private static int RecursiveCombat(Dictionary<string, int> cache, Queue<int> player1, Queue<int> player2)
        {
            var previous = new HashSet<string>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                var now1 = string.Join(",", player1.ToArray());
                var now2 = string.Join(",", player2.ToArray());

                var key = now1 + ";" + now2;
                if (previous.Contains(key))
                    return 1;

                previous.Add(key);

                var num1 = player1.Dequeue();
                var num2 = player2.Dequeue();

                if (num1 <= player1.Count && num2 <= player2.Count)
                {
                    var sub1 = player1.ToArray().Take(num1).ToArray();
                    var sub2 = player2.ToArray().Take(num2).ToArray();

                    now1 = string.Join(",", sub1.ToArray());
                    now2 = string.Join(",", sub2.ToArray());

                    key = now1 + ";" + now2;
                    if (!cache.ContainsKey(key))
                        cache.Add(key, RecursiveCombat(cache, new Queue<int>(sub1), new Queue<int>(sub2)));

                    var win = cache[key];
                    if (win == 1)
                    {
                        player1.Enqueue(num1);
                        player1.Enqueue(num2);
                    }
                    else
                    {
                        player2.Enqueue(num2);
                        player2.Enqueue(num1);
                    }
                }
                else
                {
                    if (num1 > num2)
                    {
                        player1.Enqueue(num1);
                        player1.Enqueue(num2);
                    }
                    else
                    {
                        player2.Enqueue(num2);
                        player2.Enqueue(num1);
                    }
                }
            }

            return player1.Count > 0 ? 1 : 2;
        }
    }
}
