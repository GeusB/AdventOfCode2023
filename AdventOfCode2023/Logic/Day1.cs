using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public static class Day1
    {
        public static void Execute()
        {
            var day = nameof(Day1);
            var exampleLocation = $"./Files/{day}_ex.txt";
            var example2Location = $"./Files/{day}_ex2.txt";
            var fileLocation = $"./Files/{day}.txt";

            Console.WriteLine(Part1(exampleLocation));
            Console.WriteLine(Part1(fileLocation));

            Console.WriteLine(Part2(example2Location));
            Console.WriteLine(Part2(fileLocation));
        }

        public static int Part2(string fileLocation)
        {
            var inputList = Tools.ReadListFromFile(GetLineInt, fileLocation);
            return inputList.Sum();
        }


        public static int Part1(string fileLocation)
        {
            var inputList = Tools.ReadListFromFile(GetSimpleLineInt, fileLocation);
            return inputList.Sum();
        }

        private static int GetSimpleLineInt(string line)
        {
            var lineCharInts = line.Where(char.IsDigit).Select(y => int.Parse(y.ToString())).ToArray();
            return lineCharInts.First() * 10 + lineCharInts.Last();
        }

        private static Dictionary<string, int> _numbers = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        private static Dictionary<string, int> _numbers2 = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 }
        };

        private static int GetLineIntA1(string line)
        {
            var possibleWord = "";
            var numberStrings = new List<string>();
            var numbers = new List<int>();
            foreach (var c in line)
            {
                if (char.IsDigit(c))
                {
                    if (!string.IsNullOrEmpty(possibleWord))
                    {
                        if (_numbers.ContainsKey(possibleWord))
                        {
                            numbers.Add(_numbers[possibleWord]);
                        }
                    }

                    numbers.Add(int.Parse(c.ToString()));
                    possibleWord = "";
                    continue;
                }

                if (!string.IsNullOrEmpty(possibleWord))
                {
                    if (_numbers.ContainsKey(possibleWord))
                    {
                        numbers.Add(_numbers[possibleWord]);
                        possibleWord = "";
                        continue;
                    }
                }

                possibleWord += c;
            }

            if (!string.IsNullOrEmpty(possibleWord))
            {
                numberStrings.Add(possibleWord);
            }

            foreach (var numberString in numberStrings)
            {
                if (numberString.Length == 1 && char.IsDigit(numberString.First()))
                {
                    numbers.Add(int.Parse(numberString));
                }
                else if (_numbers.ContainsKey(numberString))
                {
                    numbers.Add(_numbers[numberString]);
                }
            }

            return numbers.First() * 10 + numbers.Last();
        }

        private static int GetLineIntA2(string line)
        {
            var numbers = RecursiveNonsense(line, new List<int>());

            var lineInt = numbers.First() * 10 + numbers.Last();
            return lineInt;
        }

        private static int GetLineInt(string line)
        {
            var first = 0;
            var last = 0;
            var firstEnd = false;
            var lastEnd = false;
            for (var i = 1; i <= line.Length; i++)
            {
                if (firstEnd) break;
                var possibleMatch = line.Substring(0, i);
                foreach (var pair in _numbers2)
                {
                    if (possibleMatch.Contains(pair.Key))
                    {
                        first = pair.Value;
                        firstEnd = true;
                        break;
                    }
                }
            }

            for (var i = 1; i <= line.Length; i++)
            {
                if (lastEnd) break;
                var possibleMatch = line.Substring(line.Length - i, i);
                foreach (var pair in _numbers2)
                {
                    if (possibleMatch.Contains(pair.Key))
                    {
                        last = pair.Value;
                        lastEnd = true;
                        break;
                    }
                }
            }

            var lineInt = first * 10 + last;
            return lineInt;
        }

        private static List<int> RecursiveNonsense(string line, List<int> numbers)
        {
            var end = false;
            if (line == "")
                return numbers;
            for (var i = 1; i <= line.Length; i++)
            {
                if (end)
                    break;
                var possibleMatch = line.Substring(0, i);
                if (_numbers2.ContainsKey(possibleMatch))
                {
                    numbers.Add(_numbers2[possibleMatch]);
                    var substring = line.Substring(i, line.Length - i);
                    RecursiveNonsense(substring, numbers);
                    break;
                }

                foreach (var pair in _numbers2)
                {
                    if (possibleMatch.Contains(pair.Key))
                    {
                        numbers.Add(pair.Value);
                        var substring = line.Substring(i, line.Length - i);
                        RecursiveNonsense(substring, numbers);
                        end = true;
                        break;
                    }
                }
            }

            return numbers;
        }
    }
}