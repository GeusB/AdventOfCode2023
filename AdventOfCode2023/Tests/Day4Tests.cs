using AdventOfCode2023;
using FluentAssertions;

namespace Tests;

public class Day4Tests
{
    [Fact]
    public void Part1Ex()
    {
        var fileLocation = "./Files/Day4_ex.txt";

        Day4.Part1(fileLocation).Should().Be(13);
    }

    [Fact]
    public void Part1()
    {
        var fileLocation = "./Files/Day4.txt";

        Day4.Part1(fileLocation).Should().Be(28750);
    }

    [Fact]
    public void Part2Ex()
    {
        var fileLocation = "./Files/Day4_ex.txt";

        Day4.Part2(fileLocation).Should().Be(1312);
    }

    [Fact]
    public void Part2()
    {
        var fileLocation = "./Files/Day4.txt";

        Day4.Part2(fileLocation).Should().Be(1312);
    }
}