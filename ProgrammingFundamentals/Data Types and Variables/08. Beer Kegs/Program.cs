using System;

namespace _08._Beer_Kegs;

internal class Program
{
    static void Main(string[] args)
    {
        byte kegNum = byte.Parse(Console.ReadLine());

        string biggestModel = null;
        double biggestVolume = 0.0;

        for (byte i = 0; i < kegNum; i++) {
            string model = Console.ReadLine();
            float radius = float.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());

            double volume = Math.PI * Math.Pow(radius, 2) * height;
            if (volume > biggestVolume) {
                biggestVolume = volume;
                biggestModel = model;
            }
        }

        Console.WriteLine(biggestModel);
    }
}
