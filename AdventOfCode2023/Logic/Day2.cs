using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023;

public static class Day2
{
    private const string Day = "2";

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
        var result = inputList.Sum(y => y.GetPower());
        return result;
    }


    public static int Part1(string fileLocation)
    {
        var inputList = Tools.ReadListFromFile(GetGame, fileLocation);
        var result = inputList.Where(x => x.IsValid(12, 13, 14)).Sum(y => y.Id);
        return result;
    }

    private static Game GetGame(string line)
    {
        return new Game(line);
    }

    private class Game
     {
         public Game(string rawGame)
         {
             RawGame = rawGame;
             var split = RawGame.Split(':');
             Id = int.Parse(split.First().Split(' ').Last());
             Sets = split.Last().Split(';').Select(x => new Set(x)).ToList();
         }
     
         public int Id { get; }
         private string RawGame { get; }
         private List<Set> Sets { get; }
         private int MinRed { get; set; }
         private int MinGreen { get; set; }
         private int MinBlue { get; set; }
     
         public bool IsValid(int maxRed, int maxGreen, int maxBlue)
         {
             return Sets.All(r => r.IsValid(maxRed, maxGreen, maxBlue));
         }
     
         private int Power()
         {
             return MinRed * MinGreen * MinBlue;
         }
     
         public int GetPower()
         {
             MinRed = Sets.Max(x => x.Red);
             MinGreen = Sets.Max(x => x.Green);
             MinBlue = Sets.Max(x => x.Blue);
     
             return Power();
         }
     }

    private class Set
     {
         public Set(string rawRoundString)
         {
             var draws = rawRoundString.Split(',');
             foreach (var draw in draws)
             {
                 var drawSplit = draw.TrimStart().Split(' ');
                 switch (drawSplit.Last())
                 {
                     case "red":
                         Red = int.Parse(drawSplit.First());
                         break;
                     case "green":
                         Green = int.Parse(drawSplit.First());
                         break;
                     case "blue":
                         Blue = int.Parse(drawSplit.First());
                         break;
                 }
             }
         }
     
         public int Red { get; }
         public int Green { get; }
         public int Blue { get; }
     
         public bool IsValid(int maxRed, int maxGreen, int maxBlue)
         {
             return Red <= maxRed && Green <= maxGreen && Blue <= maxBlue;
         }
     }
}

