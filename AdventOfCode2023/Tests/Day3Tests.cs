using AdventOfCode2023;
using FluentAssertions;

namespace Tests;

public class Day3Tests
{
    [Fact]
    public void Part1Ex()
    {
        var fileLocation = "./Files/Day3_ex.txt";

        Day3.Part1(fileLocation).Should().Be(4361);
    }

    [Fact]
    public void Part1()
    {
        var fileLocation = "./Files/Day3.txt";

        Day3.Part1(fileLocation).Should().Be(527364);
    }

    [Fact]
    public void Part2Ex()
    {
        var fileLocation = "./Files/Day3_ex.txt";

        Day3.Part2(fileLocation).Should().Be(467835);
    }

    [Fact]
    public void Part2()
    {
        var fileLocation = "./Files/Day3.txt";

        Day3.Part2(fileLocation).Should().Be(79026871);
    }
}