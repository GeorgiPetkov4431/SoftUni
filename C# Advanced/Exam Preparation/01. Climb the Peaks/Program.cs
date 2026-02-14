using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Climb_the_Peaks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> peakValues = new Dictionary<string, int>()
            {
                { "Vihren", 80 },
                { "Kutelo", 90 },
                { "Banski Suhodol", 100 },
                { "Polezhan", 60 },
                { "Kamenitza", 70 }
            };

            Queue<string> peaks = new Queue<string>(peakValues.Keys);
            List<string> conqueredPeaks = new List<string>();

            Stack<int> foodPortions = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Queue<int> staminaQuantities = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            while (foodPortions.Any() && staminaQuantities.Any() && peaks.Any())
            {
                int foodPortion = foodPortions.Pop();
                int staminaQuantity = staminaQuantities.Dequeue();

                int sum = foodPortion + staminaQuantity;

                int peakDifficulty = peakValues[peaks.Peek()];

                if (sum >= peakDifficulty)
                {
                    conqueredPeaks.Add(peaks.Dequeue());
                }
            }


            if (!peaks.Any())
            {
                Console.WriteLine("Alex did it! He climbed all top five Pirin peaks in one week -> @FIVEinAWEEK");
            }
            else
            {
                Console.WriteLine("Alex failed! He has to organize his journey better next time -> @PIRINWINS");
            }

            if (conqueredPeaks.Any())
            {
                Console.WriteLine("Conquered peaks:");
                Console.WriteLine(string.Join("\n", conqueredPeaks));
            }
        }
    }
}
