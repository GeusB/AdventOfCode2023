using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2023;

public static class Day4
{
    private const string Day = "4";

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
        var inputList = Tools.ReadListFromFile(GetGame, fileLocation);
        return inputList.Sum(x => x.Points);
    }

    private static Card GetGame(string line)
    {
        return new Card(line);
    }

    public static int Part1(string fileLocation)
    {
        var inputList = Tools.ReadListFromFile(GetGame, fileLocation);
        return inputList.Sum(x => x.Points);
    }

    private class Card
    {
        private string RawValue { get; }
        private int Number { get; set; }
        private List<int> WinningNumbers { get; set; }
        private List<int> YourNumbers { get; set; }
        public int Points { get; set; }

        public Card(string rawValue)
        {
            RawValue = rawValue;
            Parse();
        }

        private void Parse()
        {
            var cardSplit = RawValue.Split(':');
            Number = int.Parse(cardSplit.First().Split(' ').Last());
            var numbers = cardSplit.Last().Split('|');
            WinningNumbers = numbers.First().Trim().Split(' ').Where(x=>x != "").Select(int.Parse).ToList();
            YourNumbers = numbers.Last().Trim().Split(' ').Where(x=>x != "").Select(int.Parse).ToList();
            var winners = YourNumbers.Where(x => WinningNumbers.Any(y => y == x));
            Points = (int)Math.Pow(2, winners.Count() - 1);
        }
    }
}