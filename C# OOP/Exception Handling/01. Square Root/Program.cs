using System;

namespace SquareRoot
{
    public class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());

            try
            {
                if (number < 0)
                {
                    throw new Exception("Invalid number.");
                }

                Console.WriteLine(Math.Sqrt(number));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
