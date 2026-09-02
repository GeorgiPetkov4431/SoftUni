namespace SetCover
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main(string[] args)
        {
            int[] universe = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            int totalSets = int.Parse(Console.ReadLine());

            List<int[]> allSets = new List<int[]>();

            for (int i = 0; i < totalSets; i++)
            {
                int[] currentSet = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                allSets.Add(currentSet);
            }

            List<int[]> chosenSets = ChooseSets(allSets, universe);

            Console.WriteLine($"Sets to take ({chosenSets.Count})");

            foreach (int[] set in chosenSets)
            {
                string setAsText = string.Join(", ", set);

                Console.WriteLine($"{{ {setAsText} }}");
            }
        }

        public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
        {
            List<int[]> result = new List<int[]>();

            while (true)
            {
                int[] maximumSet = sets
                    .OrderByDescending(s => s.Count(x => universe.Contains(x)))
                    .Take(1)
                    .FirstOrDefault();

                result.Add(maximumSet);
                sets.Remove(maximumSet);

                universe = universe
                    .Where(el => !maximumSet.Contains(el))
                    .ToList();

                if (universe.Count == 0) break;
            }

            return result;
        }
    }
}
