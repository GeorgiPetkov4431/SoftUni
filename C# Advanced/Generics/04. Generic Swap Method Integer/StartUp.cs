using System;
using System.Linq;

namespace GenericSwapMethodIntegers
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            BoxList<int> boxList = new BoxList<int>();

            for (int i = 0; i < n; i++)
            {
                Box<int> box = new Box<int>(int.Parse(Console.ReadLine()));

                boxList.Add(box);
            }

            int[] swapIndexes = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int indexOne = swapIndexes[0];
            int indexTwo = swapIndexes[1];

            boxList.Swap(indexOne, indexTwo);

            foreach (var box in boxList.Boxes)
            {
                Console.WriteLine(box);
            }
        }
    }
}
