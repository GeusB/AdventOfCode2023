using AdventOfCode2023;
using FluentAssertions;

namespace Tests;

public class Day1Tests
{
    [Fact]
    public void Part1Ex()
    {
        var day = nameof(Day1);
        var fileLocation = $"./Files/{day}_ex.txt";

        Day1.Part1(fileLocation).Should().Be(142);
    }

    [Fact]
    public void Part1()
    {
        var day = nameof(Day1);
        var fileLocation = $"./Files/{day}.txt";

        Day1.Part1(fileLocation).Should().Be(54644);
    }

    [Fact]
    public void Part2Ex2()
    {
        var day = nameof(Day1);
        var fileLocation = $"./Files/{day}_ex2.txt";

        Day1.Part2(fileLocation).Should().Be(281);
    }
    
    [Fact]
    public void Part2()
    {
        var day = nameof(Day1);
        var fileLocation = $"./Files/{day}.txt";

        Day1.Part2(fileLocation).Should().Be(53348); }

    
}