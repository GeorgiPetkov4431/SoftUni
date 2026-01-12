using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._Crossroads;

internal class Program
{
    static void Main(string[] args)
    {
        int greenLightSeconds = int.Parse(Console.ReadLine());
        int windowSeconds = int.Parse(Console.ReadLine());

        Queue<string> cars = new Queue<string>();
        bool hasCrashed = false;
        int passCount = 0;

        string command;

        while ((command = Console.ReadLine().Trim()) != "END")
        {
            if (command.ToLower() == "green" && cars.Count > 0)
            {
                int greenLight = greenLightSeconds;
                int window = windowSeconds;

                passCount++;
                string currentCar = cars.Dequeue();
                Queue<char> carSymbols = new Queue<char>(currentCar);

                while (greenLight-- > 0)
                {
                    if (carSymbols.Count == 0)
                    {
                        if (cars.Count == 0) break;
                        currentCar = cars.Dequeue();
                        passCount++;
                        carSymbols = new Queue<char>(currentCar);
                    }

                    carSymbols.Dequeue();
                }

                if (carSymbols.Count > 0)
                {
                    while (window-- > 0 && carSymbols.Count > 0)
                    {
                        carSymbols.Dequeue();
                    }

                    if (carSymbols.Count > 0)
                    {
                        hasCrashed = true;
                        Console.WriteLine("A crash happened!");
                        Console.WriteLine($"{currentCar} was hit at {carSymbols.Dequeue()}.");
                        break;
                    }
                }
            }
            else
            {
                cars.Enqueue(command);
            }
        }

        if (!hasCrashed)
        {
            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{passCount} total cars passed the crossroads.");
        }
    }
}
