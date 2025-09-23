using System;

namespace _10._Pokemon;

internal class Program
{
    static void Main(string[] args)
    {
        int originalPokePower = int.Parse(Console.ReadLine());
        int pokePower = originalPokePower; // N - poke power
        int distanceBetweenTargets = int.Parse(Console.ReadLine()); // M - distance
        int exhaustionFactor = int.Parse(Console.ReadLine()); // Y - exhaustion factor

        int totalTargetsPoked = 0;

        while (pokePower >= distanceBetweenTargets) {
            pokePower -= distanceBetweenTargets;
            totalTargetsPoked++;
            if (pokePower == originalPokePower / 2 && exhaustionFactor != 0) pokePower /= exhaustionFactor;
        }

        Console.WriteLine(pokePower);
        Console.WriteLine(totalTargetsPoked);
    }
}
