using AdventOfCode2023;

namespace Test.Project;

public class DayThreeTester
{
    [Fact]
    public void Test_Sample()
    {
        var d = SetUp("input-day3-test.txt");
        var a = d.PartB();
        Assert.Equal(467835, a);
    }

    [Fact]
    public void Test_Small()
    {
        var d= SetUp("input-day3-small.txt");
        var a = d.PartB();
        Assert.Equal(2418179, a);
    }


    private Day3 SetUp(string file)
    {
        var filePath = $"..\\..\\..\\{file}";
        var d = new Day3
        {
            lines = File.ReadAllLines(filePath).ToList()
        };
        var blank = ".".PadRight(d.lines[0].Length);
        d.lines.Insert(0, blank);
        d.lines.Add(blank);
        d.ResolveLines();
        return d;
    }
}