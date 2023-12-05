using Advent.Of.Code._2003;

namespace Test.Project
{
    public class DayOne_Tester
    {
        [Fact]
        public void Multiple_Test()
        {
            var dict = new Dictionary<string, int>
            {
                { "five1oneight", 58},
                { "two1nine", 29 },
                { "eightwothree", 83},
                { "abcone2threexyz", 13 },
                { "xtwone3four" , 24},
                { "4nineeightseven2", 42 },
                { "zoneight234" , 14},
                { "7pqrstsixteen", 76 },
                { "2zd155", 25},
                { "1", 11},
                { "1abc2", 12 },
                { "pqr3stu8vwx", 38 },
                { "a1b2c3d4e5f" , 15},
                { "treb7uchet", 77 },
                { "eighthree", 83},
                { "eightthree", 83 },
            };
            foreach (var item in dict)
            {
                var result = item.Key.ReplaceWordsWithDigit().FindDigits();
                Assert.Equal(item.Value, result);
            }
        }

        [Fact]
        public void Single_Test()
        {
            var result = "eighthree".ReplaceWordsWithDigit().FindDigits();
            Assert.Equal(83, result);
        }
    }
}