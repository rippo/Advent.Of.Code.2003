namespace AdventOfCode2023;

internal class Day2
{
    public void Go()
    {
        const string filePath = "..\\..\\..\\input-day2.txt";
        lines = File.ReadAllLines(filePath);
        ParseLines();
        PartA();
        PartB();
    }

    private void PartA()
    {
        var total = 0;
        foreach (var line in parsedLines)
        {
            if (line.Sets.Any(x => x.Red > 12))
                continue;

            if (line.Sets.Any(x => x.Green > 13))
                continue;

            if (line.Sets.Any(x => x.Blue > 14))
                continue;

            total += line.Id;
        }
        Console.WriteLine($"Day 2A: {total}");
    }

    private void PartB()
    {
        var total = 0;
        foreach (var line in parsedLines)
        {
            var minR = 0;
            var minG = 0;
            var minB = 0;

            foreach (var subset in line.Sets)
            {
                if (subset.Red > minR)
                    minR = subset.Red;
                if (subset.Green > minG)
                    minG = subset.Green;
                if (subset.Blue > minB)
                    minB = subset.Blue;
            }

            var subtotal = minR * minG * minB;
            total += subtotal;
        }
        Console.WriteLine($"Day 2B: {total}");
    }

    private void ParseLines()
    {
        //refactor this foreach loop into regex and linq
        foreach (var line in lines)
        {
            var dto = new ParsedLine();
            var parts = line.Split(':');
            dto.Id = int.Parse(parts[0].Remove(0, 5));

            var subsets = parts[1].Split(';');
            foreach (var subset in subsets)
            {
                var colors = subset.Split(',');
                var r = 0;
                var g = 0;
                var b = 0;

                foreach (var color in colors)
                {
                    if (color.Contains("red"))
                        r = int.Parse(color.Replace(" red", ""));
                    if (color.Contains("green"))
                        g = int.Parse(color.Replace(" green", ""));
                    if (color.Contains("blue"))
                        b = int.Parse(color.Replace(" blue", ""));
                }

                dto.Sets.Add(new Set
                {
                    Blue = b,
                    Red = r,
                    Green = g
                });
            }

            parsedLines.Add(dto);
        }
    }

    private string[] lines;
    private readonly List<ParsedLine> parsedLines = new();

    private record ParsedLine
    {
        public int Id { get; set; }
        public List<Set> Sets { get; } = new ();
    }

    private record Set
    {
        public int Blue { get; init; }
        public int Green { get; init; }
        public int Red { get; init; }
    }
}