using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day12
    {
        public static int PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle12.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var facing = 'E';
            var vertical = 0;
            var horizontal = 0;

            foreach (var data in lines)
            {
                var value = int.Parse(data.Substring(1));

                var action = data[0];
                switch (action)
                {
                    case 'N':
                        vertical += value;
                        break;
                    case 'S':
                        vertical -= value;
                        break;
                    case 'E':
                        horizontal += value;
                        break;
                    case 'W':
                        horizontal -= value;
                        break;
                    case 'L':
                    {
                        switch (facing)
                        {
                            case 'N':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'W';
                                        break;
                                    case 180:
                                        facing = 'S';
                                        break;
                                    case 270:
                                        facing = 'E';
                                        break;
                                }

                                break;
                            }
                            case 'S':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'E';
                                        break;
                                    case 180:
                                        facing = 'N';
                                        break;
                                    case 270:
                                        facing = 'W';
                                        break;
                                }

                                break;
                            }
                            case 'E':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'N';
                                        break;
                                    case 180:
                                        facing = 'W';
                                        break;
                                    case 270:
                                        facing = 'S';
                                        break;
                                }

                                break;
                            }
                            case 'W':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'S';
                                        break;
                                    case 180:
                                        facing = 'E';
                                        break;
                                    case 270:
                                        facing = 'N';
                                        break;
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case 'R':
                    {
                        switch (facing)
                        {
                            case 'N':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'E';
                                        break;
                                    case 180:
                                        facing = 'S';
                                        break;
                                    case 270:
                                        facing = 'W';
                                        break;
                                }

                                break;
                            }
                            case 'S':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'W';
                                        break;
                                    case 180:
                                        facing = 'N';
                                        break;
                                    case 270:
                                        facing = 'E';
                                        break;
                                }

                                break;
                            }
                            case 'E':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'S';
                                        break;
                                    case 180:
                                        facing = 'W';
                                        break;
                                    case 270:
                                        facing = 'N';
                                        break;
                                }

                                break;
                            }
                            case 'W':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'N';
                                        break;
                                    case 180:
                                        facing = 'E';
                                        break;
                                    case 270:
                                        facing = 'S';
                                        break;
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case 'F':
                    {
                        switch (facing)
                        {
                            case 'N':
                                vertical += value;
                                break;
                            case 'S':
                                vertical -= value;
                                break;
                            case 'E':
                                horizontal += value;
                                break;
                            case 'W':
                                horizontal -= value;
                                break;
                        }

                        break;
                    }
                }
            }

            return Math.Abs(vertical) + Math.Abs(horizontal);
        }

        public static long PartTwo()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle12.txt", Encoding.UTF8);
            lines = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var facing = 'E';
            var vertical = 0;
            var horizontal = 0;
            var waypointVertical = 1;
            var waypointHorizontal = 10;

            foreach (var data in lines)
            {
                var value = int.Parse(data.Substring(1));

                var action = data[0];
                switch (action)
                {
                    case 'N':
                        waypointVertical += value;
                        break;
                    case 'S':
                        waypointVertical -= value;
                        break;
                    case 'E':
                        waypointHorizontal += value;
                        break;
                    case 'W':
                        waypointHorizontal -= value;
                        break;
                    case 'L':
                    {
                        var waypointVerticalTemp = waypointVertical;
                        var waypointHorizontalTemp = waypointHorizontal;

                        switch (facing)
                        {
                            case 'N':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'W';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'S';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'E';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                            case 'S':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'E';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'N';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'W';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                            case 'E':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'N';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'W';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'S';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                            case 'W':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'S';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'E';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'N';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case 'R':
                    {
                        var waypointVerticalTemp = waypointVertical;
                        var waypointHorizontalTemp = waypointHorizontal;

                        switch (facing)
                        {
                            case 'N':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'E';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'S';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'W';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                            case 'S':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'W';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'N';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'E';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                            case 'E':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'S';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'W';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'N';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                            case 'W':
                            {
                                switch (value)
                                {
                                    case 90:
                                        facing = 'N';
                                        waypointVertical = -waypointHorizontalTemp;
                                        waypointHorizontal = waypointVerticalTemp;
                                        break;
                                    case 180:
                                        facing = 'E';
                                        waypointVertical = -waypointVerticalTemp;
                                        waypointHorizontal = -waypointHorizontalTemp;
                                        break;
                                    case 270:
                                        facing = 'S';
                                        waypointVertical = waypointHorizontalTemp;
                                        waypointHorizontal = -waypointVerticalTemp;
                                        break;
                                }

                                break;
                            }
                        }

                        break;
                    }
                    case 'F':
                    {
                        vertical += waypointVertical * value;
                        horizontal += waypointHorizontal * value;

                        break;
                    }
                }
            }

            return Math.Abs(vertical) + Math.Abs(horizontal);
        }
    }
}
