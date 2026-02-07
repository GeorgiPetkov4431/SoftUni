using System;

namespace SoftUniDataStructures
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniList list = new SoftUniList();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            for (int i = 0; i < 1_000_000; i++)
            {
                list.Add(1);
            }

            Console.WriteLine(list.GetAt(1));
        }
    }
}
