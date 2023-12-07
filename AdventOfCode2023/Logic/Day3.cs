using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2023;

public static class Day3
{
    private const string Day = "3";

    public static void Execute()
    {
        const string exampleLocation = $"./Files/Day{Day}_ex.txt";
        const string fileLocation = $"./Files/Day{Day}.txt";

        Console.WriteLine(Part1(exampleLocation));
        Console.WriteLine(Part1(fileLocation));

        Console.WriteLine(Part2(exampleLocation));
        Console.WriteLine(Part2(fileLocation));
    }

    public static int Part2(string fileLocation)
    {
        var lines = GetLines(fileLocation);

        foreach (var line in lines.Where(x => x.HasGears()))
        {
            foreach (var symbolPosition in line.GetGears())
            {
                symbolPosition.AddPartNumbersSameLine(line);
                symbolPosition.AddPartNumbersOtherLine(lines.SingleOrDefault(x => x.Number == line.Number - 1));
                symbolPosition.AddPartNumbersOtherLine(lines.SingleOrDefault(x => x.Number == line.Number + 1));
            }
        }

        return lines.Sum(x => x.GetTotalGearRatio());
    }

    private static List<Line> GetLines(string fileLocation)
    {
        var lines = new List<Line>();
        using var reader = new StreamReader(fileLocation, Encoding.Default);
        var lineNumber = 0;
        while (!reader.EndOfStream)
            lines.Add(new Line(lineNumber++, reader.ReadLine()));

        return lines;
    }


    public static int Part1(string fileLocation)
    {
        var lines = GetLines(fileLocation);

        foreach (var line in lines)
        {
            var previousLine = lines.SingleOrDefault(x => x.Number == line.Number - 1);
            var nextLine = lines.SingleOrDefault(x => x.Number == line.Number + 1);

            foreach (var numberPosition in line.NumberPositions)
            {
                if (line.SymbolPositions.Any(sp =>
                        sp.Position - 1 == numberPosition.End || sp.Position + 1 == numberPosition.Start))
                {
                    numberPosition.SetAdjacentTrue();
                }
            }

            if (previousLine != null)
            {
                foreach (var numberPosition in line.NumberPositions.Where(x => !x.IsAdjacent))
                {
                    if (previousLine.SymbolPositions.Any(sp =>
                            sp.Position >= numberPosition.Start - 1 && sp.Position <= numberPosition.End + 1))
                    {
                        numberPosition.SetAdjacentTrue();
                    }
                }
            }

            if (nextLine != null)
            {
                foreach (var numberPosition in line.NumberPositions.Where(x => !x.IsAdjacent))
                {
                    if (nextLine.SymbolPositions.Any(sp =>
                            sp.Position >= numberPosition.Start - 1 && sp.Position <= numberPosition.End + 1))
                    {
                        numberPosition.SetAdjacentTrue();
                    }
                }
            }
        }

        return lines.Sum(x => x.GetTotalValue());
    }


    private class Line
    {
        public Line(int number, string rawLine)
        {
            Number = number;
            RawLine = rawLine;
            NumberPositions = new List<NumberPosition>();
            SymbolPositions = new List<SymbolPosition>();
            Parse();
        }

        public int Number { get; }
        private string RawLine { get; }
        public List<NumberPosition> NumberPositions { get; }
        public List<SymbolPosition> SymbolPositions { get; }

        public bool HasGears() => SymbolPositions.Any(x => x.IsGear());
        public List<SymbolPosition> GetGears() => SymbolPositions.Where(x => x.IsGear()).ToList();
        public int GetTotalGearRatio() => SymbolPositions.Select(y => y.GearRatio()).Sum();
        public int GetTotalValue() => NumberPositions.Where(x => x.IsAdjacent).Sum(y => y.Value);

        private void Parse()
        {
            var rawNumber = "";
            var i = 0;
            foreach (var ch in RawLine)
            {
                if (char.IsDigit(ch))
                    rawNumber += ch;
                else
                {
                    if (rawNumber != "")
                    {
                        NumberPositions.Add(new NumberPosition(i - rawNumber.Length, i - 1, int.Parse(rawNumber)));
                        rawNumber = "";
                    }

                    if (ch != '.')
                        SymbolPositions.Add(new SymbolPosition(ch, i));
                }

                i++;
            }

            if (rawNumber != "")
            {
                NumberPositions.Add(new NumberPosition(i - rawNumber.Length, i - 1, int.Parse(rawNumber)));
            }
        }
    }

    private class NumberPosition
    {
        public NumberPosition(int start, int end, int value)
        {
            Start = start;
            End = end;
            Value = value;
            IsAdjacent = false;
        }

        public bool IsAdjacent { get; private set; }
        public int Start { get; }
        public int End { get; }
        public int Value { get; }
        public void SetAdjacentTrue() => IsAdjacent = true;
    }

    private class SymbolPosition
    {
        private char Symbol { get; }
        public int Position { get; }
        private List<int> PartNumbers { get; }
        public bool IsGear() => Symbol == '*';
        private bool IsValidGear() => IsGear() && PartNumbers.Count == 2;
        public int GearRatio() => IsValidGear() ? PartNumbers.First() * PartNumbers.Last() : 0;
        private void AddNumbers(IEnumerable<int> numbers) => PartNumbers.AddRange(numbers);

        public void AddPartNumbersOtherLine(Line otherLine)
        {
            if (otherLine == null) return;
            AddNumbers(otherLine.NumberPositions
                .Where(np => np.Start - 1 <= Position && np.End + 1 >= Position)
                .Select(y => y.Value).ToList());
        }

        public void AddPartNumbersSameLine(Line sameLine)
        {
            AddNumbers(sameLine.NumberPositions
                .Where(sp => sp.Start == Position + 1 || sp.End == Position - 1)
                .Select(y => y.Value).ToList());
        }

        public SymbolPosition(char symbol, int position)
        {
            Symbol = symbol;
            Position = position;
            PartNumbers = new List<int>();
        }
    }
}