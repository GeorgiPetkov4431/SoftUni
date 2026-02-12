using System;
using System.Linq;

namespace GenericSwapMethodStrings
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            BoxList<string> boxList = new BoxList<string>();

            for (int i = 0; i < n; i++)
            {
                Box<string> box = new Box<string>(Console.ReadLine());

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
