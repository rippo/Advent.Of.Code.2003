using Advent.Of.Code._2003;

namespace Test.Project
{
    public class DayOneTester
    {
        [Fact]
        public void Multiple_PartB_Test()
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
            var expectedResult = dict.Values.ToArray().ToList().Sum(w => w);
            var d = new Day1
            {
                Lines = dict.Keys.ToArray()
            };
            Assert.Equal(expectedResult, d.PartB());
        }

        [Fact]
        public void Single_PartA_Test()
        {
            string[] input = { "123RIPPO456" };
            var d = new Day1
            {
                Lines = input
            };
            Assert.Equal(16, d.PartA());
        }


        [Fact]
        public void Single_PartB_Test()
        {
            string[] input = { "eighthree" };
            var d = new Day1
            {
                Lines = input
            };
            Assert.Equal(83, d.PartB());
        }
    }
}