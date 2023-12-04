using AdventOfCode2023;
using FluentAssertions;

namespace Tests;

public class Day2Tests
{
    [Fact]
    public void Part1Ex()
    {
        var fileLocation = "./Files/Day2_ex.txt";

        Day2.Part1(fileLocation).Should().Be(8);
    }

    [Fact]
    public void Part1()
    {
        var fileLocation = "./Files/Day2.txt";

        Day2.Part1(fileLocation).Should().Be(2204);
    }

    [Fact]
    public void Part2Ex()
    {
        var fileLocation = "./Files/Day2_ex.txt";

        Day2.Part2(fileLocation).Should().Be(2286);
    }

    [Fact]
    public void Part2()
    {
        var fileLocation = "./Files/Day2.txt";

        Day2.Part2(fileLocation).Should().Be(71036);
    }
}