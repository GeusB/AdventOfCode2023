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
        return 0;
    }


    public static int Part1(string fileLocation)
    {
        return 0;
    }
}