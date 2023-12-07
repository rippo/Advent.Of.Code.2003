using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day3
{
    public void Go()
    {
        var filePath = $"..\\..\\..\\input-day3.txt";
        lines = File.ReadAllLines(filePath).ToList();

        //add blanks row top and bottom
        var blank = ".".PadRight(lines[0].Length);
        lines.Insert(0, blank);
        lines.Add(blank);

        ResolveLines();
        PartA();
        PartB();
        
    }

    public int PartB()
    {
        var grandTotal = 0;

        for (var i = 1; i < lines.Count - 1; i++)
        {
            var i1 = i;
            var gearsOnLine = parsedSymbols.Where(w => w.LineNumber == i1 && w.Symbol == '*');
            foreach (var gear in gearsOnLine)
            {
                var matches = new List<int>();

                //Get all numbers on line above and below
                var numbersToCheck = parsedNumbers.Where(w => w.LineNumber >= gear.LineNumber - 1 && w.LineNumber <= gear.LineNumber + 1);

                foreach (var numberToCheck in numbersToCheck)
                {
                    var numberLen = numberToCheck.Number.ToString().Length;

                    //number is in range of gear -len of gear to +1
                    var lowPos = gear.SymbolPosition - numberLen;
                    var highPos = gear.SymbolPosition + 1;

                    if (numberToCheck.NumberPosition >= lowPos &&
                        numberToCheck.NumberPosition <= highPos)
                    {
                        matches.Add(numberToCheck.Number);
                        if (matches.Count == 2)
                            break;
                    }
                }

                if (matches.Count == 2)
                {
                    grandTotal += matches[0] * matches[1];
                }
            }
        }

        Console.WriteLine($"Day 3B: {grandTotal}");
        return grandTotal;
    }

    public void ResolveLines()
    {
        for (var i = 0; i < lines.Count - 1; i++)
        {
            ResolveParsedPieces(i);
        }
    }

    private void PartA()
    {
        var grandTotal = 0;
        for (var i = 1; i < lines.Count - 1; i++)
        {
            foreach (var parsedNumber in parsedNumbers.Where(w => w.LineNumber == i))
            {
                var lenNumber = parsedNumber.Number.ToString().Length;

                //Line above
                foreach (var symbol in parsedSymbols.Where(w => w.LineNumber == i - 1))
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
                    if (symbolPos == parsedNumber.NumberPosition - 1 ||
                        symbolPos == parsedNumber.NumberPosition + lenNumber)
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
        Console.WriteLine($"Day 3A: {grandTotal}");
    }

    private void ResolveParsedPieces(int lineNumber)
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

    public List<string> lines = new();
    private readonly List<ParsedNumbers> parsedNumbers = new();
    private readonly List<ParsedSymbols> parsedSymbols = new();

    private record ParsedNumbers(int LineNumber, int Number, int NumberPosition);

    private record ParsedSymbols(int LineNumber, char Symbol, int SymbolPosition);
}