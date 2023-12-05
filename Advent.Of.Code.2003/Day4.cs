﻿namespace Advent.Of.Code._2003
{
    internal class Day4
    {
        public static void Go(string empty)
        {
            PartA();
            PartB();
        }

        private static string[] GetNumbers(string line, int part)
        {
            return line.Split(':')[1].Split('|')[part].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        private static void PartA()
        {
            var pointSum = 0;
            var filePath = $"..\\..\\..\\input-day4.txt";

            foreach (string line in File.ReadAllLines(filePath))
            {
                var winningNums = GetNumbers(line, 0);
                var scratchNums = GetNumbers(line, 1);
                var wonNums = scratchNums.Intersect(winningNums);
                pointSum += (int)Math.Pow(2, wonNums.Count() - 1);
            }

            //23678
            Console.WriteLine($"Part 4a: {pointSum}");
        }

        private static void PartB()
        {
            var filePath = $"..\\..\\..\\input-day4.txt";
            var lines = File.ReadAllLines(filePath);

            var cards = Enumerable.Repeat(1, lines.Length).ToArray();
            for (var i = 0; i < lines.Length; i++)
            {
                var winningNums = GetNumbers(lines[i], 0);
                var scratchNums = GetNumbers(lines[i], 1);

                var points = scratchNums.Intersect(winningNums).Count();
                for (var j = i + 1; j < i + points + 1; ++j)
                {
                    cards[j] += cards[i];
                }
            }

            //15455663
            Console.WriteLine($"Part 4b: {cards.Sum()}");
        }
    }
}