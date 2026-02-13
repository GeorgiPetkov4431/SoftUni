using System;
using System.Linq;

namespace Froggy
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            int[] stones = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            Lake lake = new Lake(stones);

            Console.WriteLine(string.Join(", ", lake));
        }
    }
}
