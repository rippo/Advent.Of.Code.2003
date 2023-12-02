namespace Advent.Of.Code._2003;

public static class DigitExtensions
{
    public static int FindDigits(this string input)
    {
        return int.Parse($"{input.FindFirstDigit()}{input.FindLastDigit()}");
    }

    public static char FindFirstDigit(this string input)
    {
        return input.FirstOrDefault(char.IsDigit);
    }

    public static char FindLastDigit(this string input)
    {
        return input.LastOrDefault(char.IsDigit);
    }

    public static string ReplaceWordsWithDigit(this string line)
    {
        var foundPositions = new Dictionary<int, KeyValuePair<string, string>>();

        //first one
        foreach (var replacement in Replacements)
        {
            var indexOf = line.IndexOf(replacement.Key, StringComparison.Ordinal);
            if (indexOf > -1)
            {
                foundPositions.Add(line.IndexOf(replacement.Key, StringComparison.Ordinal), replacement);
            }
        }

        //foundPositions now contains the found numbers
        if (foundPositions.Any())
        {
            var index = foundPositions.MinBy(w => w.Key);
            line = line.Replace(index.Value.Key, index.Value.Value);
        }

        //find last one, lets utilise reverse, and basically do the same above
        var reverseLine = line.ReverseString();
        foundPositions = new Dictionary<int, KeyValuePair<string, string>>();
        foreach (var replacement in Replacements)
        {
            var reverseKey = replacement.Key.ReverseString();
            var indexOf = reverseLine.IndexOf(reverseKey, StringComparison.Ordinal);
            if (indexOf > -1)
                foundPositions.Add(indexOf, replacement);
        }
        //foundPositions now contains the found numbers, get first one and replace
        if (foundPositions.Any())
        {
            var index = foundPositions.MinBy(w => w.Key);
            line = reverseLine.ReverseString().Replace(index.Value.Key, index.Value.Value);
        }

        return line;
    }

    public static string ReverseString(this string input)
    {
        return new string(input.Reverse().ToArray());
    }

    private static readonly Dictionary<string, string> Replacements = new()
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };
}