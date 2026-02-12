using System;

namespace GenericCountMethodStrings
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            BoxList<string> boxes = new BoxList<string>();

            for (int i = 0; i < n; i++)
            {
                boxes.Add(new Box<string>(Console.ReadLine()));
            }

            Box<string> element = new Box<string>(Console.ReadLine());

            Console.WriteLine(boxes.Compare(element));
        }
    }
}
