using System;

namespace _07._Water_Overflow;
internal class Program
{
    static void Main(string[] args)
    {
        byte interval = byte.Parse(Console.ReadLine());

        short totalLiters = 0;

        for (byte i  = 0; i < interval; i++) {
            short liters = short.Parse(Console.ReadLine());
            if (totalLiters + liters > 255) Console.WriteLine($"Insufficient capacity!");
            else totalLiters+= liters;
        }

        Console.WriteLine(totalLiters);
    }
}
