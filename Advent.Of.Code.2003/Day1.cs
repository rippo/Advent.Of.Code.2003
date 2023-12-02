namespace Advent.Of.Code._2003;

internal static class Day1
{
    public static void Go()
    {
        var filePath = "..\\..\\..\\input-day1.txt";
        lines = File.ReadAllLines(filePath);
        PartA();
        PartB();
    }

    private static void PartA()
    {
        var total = 0;
        foreach (var line in lines)
        {
            var firstDigit = line.FindFirstDigit();
            var lastDigit = line.FindLastDigit();
            total += int.Parse($"{firstDigit}{lastDigit}");
        }

        Console.WriteLine(total);
    }

    private static void PartB()
    {
        var total = 0;
        foreach (var line in lines)
        {
            var firstDigit = line.ReplaceWordsWithDigit().FindFirstDigit();
            var lastDigit = line.ReplaceWordsWithDigit().FindLastDigit();
            total += int.Parse($"{firstDigit}{lastDigit}");
        }

        Console.WriteLine(total);
    }

    private static string[] lines;
}