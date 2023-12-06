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

        foreach (var line in lines)
        {
            if (!line.SymbolPositions.Any(x=>x.IsGear())) 
                continue;
            
            var previousLine = lines.SingleOrDefault(x => x.Number == line.Number - 1);
            var nextLine = lines.SingleOrDefault(x => x.Number == line.Number + 1);

            foreach (var symbolPosition in line.SymbolPositions.Where(x=>x.IsGear()))
            {
                var partNumbers = line.NumberPositions.Where(sp =>
                        sp.End == symbolPosition.Position - 1 || sp.Start == symbolPosition.Position + 1)
                    .Select(y => y.Value).ToList();
                if (partNumbers.Any())
                {
                    symbolPosition.AddNumbers(partNumbers);
                    
                }

                if (previousLine != null)
                {
                    var previousLineNumbers = previousLine.NumberPositions
                        .Where(np => np.Start - 1 <= symbolPosition.Position && np.End + 1 >= symbolPosition.Position)
                        .Select(y => y.Value).ToList();
                    if (previousLineNumbers.Any())
                    {
                        symbolPosition.AddNumbers(previousLineNumbers);
                    }
                }
                
                if (nextLine != null)
                {
                    var nextLineNumbers = nextLine.NumberPositions
                        .Where(np => np.Start - 1 <= symbolPosition.Position && np.End + 1 >= symbolPosition.Position)
                        .Select(y => y.Value).ToList();
                    if (nextLineNumbers.Any())
                    {
                        symbolPosition.AddNumbers(nextLineNumbers);
                    }
                }
            }
        }

        return lines.Sum(x => x.SymbolPositions.Select(y=>y.GearRatio()).Sum());
    }

    private static List<Line> GetLines(string fileLocation)
    {
        var lines = new List<Line>();
        using var reader = new StreamReader(fileLocation, Encoding.Default);
        var lineNumber = 0;
        while (!reader.EndOfStream)
            lines.Add(new Line(lineNumber++, reader.ReadLine()));

        lines.ForEach(x => x.Parse());
        return lines;
    }


    public static int Part1(string fileLocation)
    {
        var lines = new List<Line>();
        using var reader = new StreamReader(fileLocation, Encoding.Default);
        var lineNumber = 0;
        while (!reader.EndOfStream)
            lines.Add(new Line(lineNumber++, reader.ReadLine()));

        lines.ForEach(x => x.Parse());

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

        return lines.Sum(x => x.GetValue());
    }
}


class Line
{
    public Line(int number, string rawLine)
    {
        Number = number;
        RawLine = rawLine;
        NumberPositions = new List<NumberPosition>();
        SymbolPositions = new List<SymbolPosition>();
    }

    public int Number { get; }
    private int LineValue { get; set; }
    private string RawLine { get; }
    public List<NumberPosition> NumberPositions { get; }
    public List<SymbolPosition> SymbolPositions { get; }

    public void AddValue(int value) => LineValue += value;
    public int GetValue() => LineValue = NumberPositions.Where(x => x.IsAdjacent).Sum(y => y.Value);

    public void Parse()
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

class NumberPosition
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
    public string RawValue { get; set; }

    public void SetAdjacentTrue()
    {
        IsAdjacent = true;
    }
}

class SymbolPosition
{
    private char Symbol { get; }
    public int Position { get; }
    private List<int> PartNumbers { get; }
    public bool IsGear() => Symbol == '*';
    private bool IsValidGear() => IsGear() && PartNumbers.Count == 2;
    public int GearRatio() => IsValidGear() ? PartNumbers.First() * PartNumbers.Last() : 0;
    public void AddNumbers(IEnumerable<int> numbers) => PartNumbers.AddRange(numbers);

    
    
    public SymbolPosition(char symbol, int position)
    {
        Symbol = symbol;
        Position = position;
        PartNumbers = new List<int>();
    }
}