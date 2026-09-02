namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int[] coins = Console
                .ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .OrderByDescending(c => c)
                .ToArray();

            int targetSum = int.Parse(Console.ReadLine());

            Dictionary<int, int> result = ChooseCoins(coins, targetSum);

            if (result == null)
            {
                Console.WriteLine("Error");
                return;
            }

            int totalCoins = result.Values.Sum();

            Console.WriteLine($"Number of coins to take: {totalCoins}");

            foreach ((int key, int value) in result)
            {
                Console.WriteLine($"{value} coin(s) with value {key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            foreach (int coin in coins)
            {
                int count = targetSum / coin;
            
                if (count > 0)
                {
                    result[coin] = count;

                    targetSum %= coin;
                    
                    if (targetSum == 0)
                    {
                        return result;
                    }
                }
            }

            if (targetSum != 0)
            {
                return null;
            }

            return result;
        }
    }
}