namespace Advent.Of.Code._2003
{
    public class Day5
    {
        public void Go()
        {
            var filePath = $"..\\..\\..\\input-day5.txt";
            var lines = File.ReadAllLines(filePath);

            seeds = Array.ConvertAll(lines[0].Remove(0, 7).Split(' '), long.Parse);

            maps = new List<Func<long, long>>
            {
                BuildMap(lines[3..36]),
                BuildMap(lines[38..62]),
                BuildMap(lines[64..86]),
                BuildMap(lines[88..107]),
                BuildMap(lines[109..120]),
                BuildMap(lines[122..131]),
                BuildMap(lines[133..165])
            };

            PartA();
            PartB();
        }

        private Func<long, long> BuildMap(string[] lines)
        {
            var ranges = new List<long[]>();

            foreach (var line in lines)
            {
                ranges.Add(Array.ConvertAll(line.Split(' '), long.Parse));
            }

            return (x =>
            {
                foreach (var range in ranges)
                    if (x >= range[1] && x < range[1] + range[2])
                        return x + (range[0] - range[1]);
                return x;
            });
        }

        private void PartA()
        {
            var locations = new List<long>();
            foreach (var seed in seeds)
            {
                var lookup = seed;
                foreach (var map in maps)
                {
                    lookup = map(lookup);
                }

                //adds the last lookup to the list of locations
                locations.Add(lookup);
            }

            Console.WriteLine($"Day 5A: {locations.Min()}");
        }

        private void PartB()
        {
            Console.WriteLine("Day 5B: Skiping as it takes to long to compute Answer was 125742456");

            var locations = new List<long>();
            for (var i = 0; i < seeds.Length; i += 2)
            {
                var initial = seeds[i];
                var final = initial + seeds[i + 1];
                //This takes over 30 mins to run!
                for (var seed = initial; seed < final; seed++)
                {
                    var lookup = seed;
                    foreach (var map in maps)
                    {
                        lookup = map(lookup);
                    }

                    //adds the last lookup to the list of locations
                    locations.Add(lookup);
                }
            }

            Console.WriteLine($"Day 5B: {locations.Min()}");
        }

        private List<Func<long, long>> maps;
        private long[] seeds;
    }
}