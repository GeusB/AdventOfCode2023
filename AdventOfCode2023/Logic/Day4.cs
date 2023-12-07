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


        foreach (var card in inputList)
        {
            if (card.WinnerCount == 0) continue;

            for (var w = 1; w <= card.WinnerCount; w++)
            {
                var cardToUpdate = inputList.Single(x => x.Number == card.Number + w);
                cardToUpdate.Add(card.Count);
            }
        }

        return inputList.Sum(x => x.Count);
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
        public int Number { get; set; }
        private List<int> WinningNumbers { get; set; }
        private List<int> YourNumbers { get; set; }
        public int Points { get; private set; }
        public int WinnerCount { get; private set; }
        public int Count { get; private set; }

        public Card(string rawValue)
        {
            RawValue = rawValue;
            Parse();
            Count = 1;
        }

        private void Parse()
        {
            var cardSplit = RawValue.Split(':');
            Number = int.Parse(cardSplit.First().Split(' ').Last());
            var numbers = cardSplit.Last().Split('|');
            WinningNumbers = numbers.First().Trim().Split(' ').Where(x => x != "").Select(int.Parse).ToList();
            YourNumbers = numbers.Last().Trim().Split(' ').Where(x => x != "").Select(int.Parse).ToList();
            var winners = YourNumbers.Where(x => WinningNumbers.Any(y => y == x));
            WinnerCount = winners.Count();
            Points = (int)Math.Pow(2, WinnerCount - 1);
        }

        public void Add(int count) => Count+= count;
    }
}