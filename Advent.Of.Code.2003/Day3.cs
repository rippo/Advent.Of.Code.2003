using System.Text.RegularExpressions;

namespace Advent.Of.Code._2003;

public static class Day3
{
    public static void Go(string file)
    {
        var filePath = $"..\\..\\..\\{file}";
        lines = File.ReadAllLines(filePath).ToList();

        //add a blank row top and bottom
        var blank = ".".PadRight(lines[0].Length);
        lines.Insert(0, blank);
        lines.Add(blank);

        for (var i = 0; i < lines.Count - 1; i++)
        {
            ResolveParsedPieces(i);
        }

        PartA();
        PartB();
    }

    private static void ResolveParsedPieces(int lineNumber)
    {
        var pattern = @"\d+";
        var matches = Regex.Matches(lines[lineNumber], pattern);
        foreach (Match match in matches)
        {
            parsedNumbers.Add(new ParsedNumbers(lineNumber, int.Parse(match.Value), match.Index));
        }

        pattern = @"[^0-9\. ]";
        matches = Regex.Matches(lines[lineNumber], pattern, RegexOptions.IgnorePatternWhitespace);
        foreach (Match match in matches)
        {
            parsedSymbols.Add(new ParsedSymbols(lineNumber, match.Value[0], match.Index));
        }
    }

    private static void PartA()
    {
        var grandTotal = 0;
        var grandTotal2 = 0;
        for (var i = 1; i < lines.Count - 1; i++)
        {
            foreach (var parsedNumber in parsedNumbers.Where(w=>w.LineNumber == i))
            {
                var lenNumber = parsedNumber.Number.ToString().Length;

                //Line above
                foreach (var symbol in parsedSymbols.Where(w=>w.LineNumber == i - 1))
                {
                    var symbolPos = symbol.SymbolPosition;
                    if (symbolPos >= parsedNumber.NumberPosition - 1 &&
                        symbolPos <= parsedNumber.NumberPosition + lenNumber)
                        grandTotal += parsedNumber.Number;
                }

                //current line
                foreach (var symbol in parsedSymbols.Where(w => w.LineNumber == i))
                {
                    var symbolPos = symbol.SymbolPosition;
                    if (symbolPos == parsedNumber.NumberPosition - 1  || 
                        symbolPos == parsedNumber.NumberPosition + lenNumber )
                        grandTotal += parsedNumber.Number;
                }


                //line below
                foreach (var symbol in parsedSymbols.Where(w => w.LineNumber == i + 1))
                {
                    var symbolPos = symbol.SymbolPosition;
                    if (symbolPos >= parsedNumber.NumberPosition - 1 &&
                        symbolPos <= parsedNumber.NumberPosition + lenNumber)
                        grandTotal += parsedNumber.Number;
                }
            }

        }
        //556057
        Console.WriteLine(grandTotal);
    }

    private static void PartB()
    {
        var grandTotal = 0;
        var stars = parsedSymbols.Where(w => w.Symbol == '*').ToList();

        for (var i = 1; i < lines.Count - 1; i++)
        {
            foreach (var symbol in stars.Where(w => w.LineNumber == i))
            {
                var symbolPos = symbol.SymbolPosition;

                var topNumber = parsedNumbers.FirstOrDefault(w => w.LineNumber == i - 1 &&
                                                                  w.NumberPosition >= symbolPos - w.Number.ToString().Length &&
                                                                  w.NumberPosition <= symbolPos + 1);

                var bottomNumber = parsedNumbers.FirstOrDefault(w => w.LineNumber == i + 1 &&
                                                                  w.NumberPosition >= symbolPos - w.Number.ToString().Length &&
                                                                  w.NumberPosition <= symbolPos + 1);

                if (topNumber != null && bottomNumber != null)
                {
                    grandTotal += topNumber.Number * bottomNumber.Number;
                }

            }
        }
        Console.WriteLine($"WRONG Part 3b: {grandTotal}");


        int sum = 0;
        Match[][] numGrid = new Match[142][];
        for (int i = 1; i < lines.Count - 1; ++i)
        {
            numGrid[i] = new Match[142];
            MatchCollection numbers = Regex.Matches(lines[i], @"\d+");
            foreach (Match number in numbers)
            {
                for (int j = number.Index; j < number.Index + number.Length; ++j)
                {
                    numGrid[i][j] = number;
                }
            }
        }

        for (int i = 1; i < lines.Count - 1; ++i)
        {
            MatchCollection gears = Regex.Matches(lines[i], @"\*");
            foreach (Match gear in gears)
            {
                Dictionary<Match, int> partNums = new Dictionary<Match, int>();
                foreach (Match[] row in numGrid[(i - 1)..(i + 2)])
                {
                    if (row == null) continue;

                    foreach (Match m in row[(gear.Index - 1)..(gear.Index + 2)])
                    {
                        if (m != null && !partNums.ContainsKey(m))
                        {
                            partNums[m] = int.Parse(m.Value);
                        }
                    }
                }

                if (partNums.Count == 2)
                {
                    int[] nums = partNums.Values.ToArray();
                    sum += nums[0] * nums[1];
                }
            }
        }
        
        //82824352 RIGHT HOW TO GET THIS
        Console.WriteLine($"Part 3b: {sum}");

    }

    private static List<string> lines = new();
    private static readonly List<ParsedNumbers> parsedNumbers = new();
    private static readonly List<ParsedSymbols> parsedSymbols = new();

    private record ParsedNumbers(int LineNumber, int Number, int NumberPosition);

    private record ParsedSymbols(int LineNumber, char Symbol, int SymbolPosition);
}