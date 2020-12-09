using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day04
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle04.txt", Encoding.UTF8);

            var num = 0;

            string batch = string.Empty;
            foreach (var data in lines)
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    batch += " " + data;
                    continue;
                }

                if (Present(batch, false))
                {
                    ++num;
                }

                batch = string.Empty;
            }

            if (Present(batch, false))
            {
                ++num;
            }

            return num;
        }

        public static int PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle04.txt", Encoding.UTF8);

            var num = 0;

            string batch = string.Empty;
            foreach (var data in lines)
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    batch += " " + data;
                    continue;
                }

                if (Present(batch, true))
                {
                    ++num;
                }

                batch = string.Empty;
            }

            if (Present(batch, true))
            {
                ++num;
            }

            return num;
        }

        private static bool Present(string batch, bool valid)
        {
            if (!string.IsNullOrWhiteSpace(batch))
            {
                var pass = batch.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1]);

                if (pass.ContainsKey("byr") && pass.ContainsKey("iyr") && pass.ContainsKey("eyr") && pass.ContainsKey("hgt") && pass.ContainsKey("hcl") && pass.ContainsKey("ecl") && pass.ContainsKey("pid")) // && pass.ContainsKey("cid")
                {
                    if (valid)
                    {
                        int num;

                        if (pass["byr"].Length != 4 || !int.TryParse(pass["byr"], NumberStyles.None, CultureInfo.InvariantCulture, out num) || num < 1920 || num > 2002)
                        {
                            return false;
                        }

                        if (pass["iyr"].Length != 4 || !int.TryParse(pass["iyr"], NumberStyles.None, CultureInfo.InvariantCulture, out num) || num < 2010 || num > 2020)
                        {
                            return false;
                        }

                        if (pass["eyr"].Length != 4 || !int.TryParse(pass["eyr"], NumberStyles.None, CultureInfo.InvariantCulture, out num) || num < 2020 || num > 2030)
                        {
                            return false;
                        }

                        if (pass["hgt"].Length < 4 || !int.TryParse(pass["hgt"].Substring(0, pass["hgt"].Length - 2), NumberStyles.None, CultureInfo.InvariantCulture, out num))
                        {
                            return false;
                        }

                        if (pass["hgt"].EndsWith("cm"))
                        {
                            if (num < 150 || num > 193)
                            {
                                return false;
                            }
                        }
                        else if (pass["hgt"].EndsWith("in"))
                        {
                            if (num < 59 || num > 76)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                        if (pass["hcl"].Length != 7 || pass["hcl"][0] != '#' || !int.TryParse(pass["hcl"].Substring(1, 6), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out num))
                        {
                            return false;
                        }

                        if (!"amb blu brn gry grn hzl oth".Split(' ').Contains(pass["ecl"]))
                        {
                            return false;
                        }

                        if (pass["pid"].Length != 9 || !int.TryParse(pass["pid"], NumberStyles.None, CultureInfo.InvariantCulture, out num))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
