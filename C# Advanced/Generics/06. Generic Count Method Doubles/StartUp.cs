using System;

namespace GenericCountMethodDoubles
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            BoxList<double> boxes = new BoxList<double>();

            for (int i = 0; i < n; i++)
            {
                boxes.Add(new Box<double>(double.Parse(Console.ReadLine())));
            }

            Box<double> element = new Box<double>(double.Parse(Console.ReadLine()));

            Console.WriteLine(boxes.Compare(element));
        }
    }
}
