using System;
using System.Numerics;

namespace _11._Snowballs;
internal class Program
{
    static void Main(string[] args)
    {
        int snowballNumber = int.Parse(Console.ReadLine());
        int bestSnowballSnow = 0, bestSnowballTime = 0, bestSnowballQuality = 0;
        BigInteger bestSnowballValue = 0;

        for (int i = 0; i < snowballNumber; i++) {
            int snowballSnow = int.Parse(Console.ReadLine());
            int snowballTime = int.Parse(Console.ReadLine());
            int snowballQuality = int.Parse(Console.ReadLine());

            BigInteger snowballValue = BigInteger.Pow(snowballSnow / snowballTime, snowballQuality);

            if (snowballValue > bestSnowballValue) {
                bestSnowballSnow = snowballSnow;
                bestSnowballTime = snowballTime;
                bestSnowballQuality = snowballQuality;
                bestSnowballValue = snowballValue;
            }
        }

        Console.WriteLine($"{bestSnowballSnow} : {bestSnowballTime} = {bestSnowballValue} ({bestSnowballQuality})");
    }
}
