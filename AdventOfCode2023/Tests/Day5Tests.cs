using AdventOfCode2023;
using FluentAssertions;

namespace Tests;

public class Day5Tests
{
    [Fact]
    public void Part1Ex()
    {
        var fileLocation = "./Files/Day5_ex.txt";

        Day5.Part1(fileLocation).Should().Be(35);
    }

    [Fact]
    public void Part1()
    {
        var fileLocation = "./Files/Day5.txt";

        Day5.Part1(fileLocation).Should().Be(28750);
    }

    [Fact]
    public void Part2Ex()
    {
        var fileLocation = "./Files/Day5_ex.txt";

        Day5.Part2(fileLocation).Should().Be(30);
    }

    [Fact]
    public void Part2()
    {
        var fileLocation = "./Files/Day5.txt";

        Day5.Part2(fileLocation).Should().Be(10212704);
    }
}