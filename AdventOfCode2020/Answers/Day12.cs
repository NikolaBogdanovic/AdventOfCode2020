using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Answers
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
                                facing = "NWSE"[value / 90];
                                break;
                            case 'S':
                                facing = "SENW"[value / 90];
                                break;
                            case 'E':
                                facing = "ENWS"[value / 90];
                                break;
                            case 'W':
                                facing = "WSEN"[value / 90];
                                break;
                        }

                        break;
                    }
                    case 'R':
                    {
                        switch (facing)
                        {
                            case 'N':
                                facing = "NESW"[value / 90];
                                break;
                            case 'S':
                                facing = "SWNE"[value / 90];
                                break;
                            case 'E':
                                facing = "ESWN"[value / 90];
                                break;
                            case 'W':
                                facing = "WNES"[value / 90];
                                break;
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

        public static int PartTwo()
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
                        switch (facing)
                        {
                            case 'N':
                                facing = "NWSE"[value / 90];
                                break;
                            case 'S':
                                facing = "SENW"[value / 90];
                                break;
                            case 'E':
                                facing = "ENWS"[value / 90];
                                break;
                            case 'W':
                                facing = "WSEN"[value / 90];
                                break;
                        }

                        var waypointVerticalOld = waypointVertical;
                        var waypointHorizontalOld = waypointHorizontal;

                        switch (value)
                        {
                            case 90:
                                waypointVertical = waypointHorizontalOld;
                                waypointHorizontal = -waypointVerticalOld;
                                break;
                            case 180:
                                waypointVertical = -waypointVerticalOld;
                                waypointHorizontal = -waypointHorizontalOld;
                                break;
                            case 270:
                                waypointVertical = -waypointHorizontalOld;
                                waypointHorizontal = waypointVerticalOld;
                                break;
                        }

                        break;
                    }
                    case 'R':
                    {
                        switch (facing)
                        {
                            case 'N':
                                facing = "NESW"[value / 90];
                                break;
                            case 'S':
                                facing = "SWNE"[value / 90];
                                break;
                            case 'E':
                                facing = "ESWN"[value / 90];
                                break;
                            case 'W':
                                facing = "WNES"[value / 90];
                                break;
                        }

                        var waypointVerticalOld = waypointVertical;
                        var waypointHorizontalOld = waypointHorizontal;

                        switch (value)
                        {
                            case 90:
                                facing = 'N';
                                waypointVertical = -waypointHorizontalOld;
                                waypointHorizontal = waypointVerticalOld;
                                break;
                            case 180:
                                facing = 'E';
                                waypointVertical = -waypointVerticalOld;
                                waypointHorizontal = -waypointHorizontalOld;
                                break;
                            case 270:
                                facing = 'S';
                                waypointVertical = waypointHorizontalOld;
                                waypointHorizontal = -waypointVerticalOld;
                                break;
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
