using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AdventOfCode2023
{
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
            return 0;
        }


        public static int Part1(string fileLocation)
        {
            
            var inputList = Tools.ReadListFromFile(x=> GetGame(x,12,13,14), fileLocation);
            var result = inputList.Where(x => x.IsValid()).Sum(y => y.Id);
            return result;
        }
        
        private static Game GetGame(string line,int maxRed, int maxGreen, int maxBlue)
        {
            return new Game(maxRed, maxGreen, maxBlue, line);
        }
    }

    class Game
    {
        public int Id { get; set; }
        private int MaxRed { get; set; }
        private int MaxGreen { get; set; }
        private int MaxBlue { get; set; }
        private string RawGame { get; set; }
        
        public List<Round> Rounds { get; set; }

        public bool IsValid() => Rounds.All(r => r.IsValid);
        
        public Game(int maxRed, int maxGreen, int maxBlue, string rawGame)
        {
            MaxRed = maxRed;
            MaxGreen = maxGreen;
            MaxBlue = maxBlue;
            RawGame = rawGame;
            var split = RawGame.Split(':');
            Id = int.Parse(split.First().Split(' ').Last());
            Rounds = split.Last().Split(';').Select(x => new Round(x, MaxRed, MaxGreen, MaxBlue)).ToList();
        }
    }

    class Round
    {
        private int Red { get; set; }
        private int Green { get; set; }
        private int Blue { get; set; }

        public bool IsValid { get; set; }
        
        public bool GetIsValid(int maxRed, int maxGreen, int maxBlue) => Red <= maxRed && Green <= maxGreen && Blue <= maxBlue;
        
        
        public Round(string rawRoundString, int maxRed, int maxGreen, int maxBlue)
        {
            var draws = rawRoundString.Split(',');
            foreach (var draw in draws)
            {
                var drawSplit = draw.TrimStart().Split(' ');
                switch (drawSplit.Last())
                {
                    case "red": Red = int.Parse(drawSplit.First());
                        break;
                    case "green": Green = int.Parse(drawSplit.First());
                        break;
                    case "blue": Blue = int.Parse(drawSplit.First());
                        break;
                    
                }
            }
            IsValid = Red <= maxRed && Green <= maxGreen && Blue <= maxBlue;
        }
    }
}