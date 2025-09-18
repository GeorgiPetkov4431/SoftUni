using System;

namespace Basic_Syntax__Conditional_Statements_and_Loops___Exercise;

internal class Program
{
    static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());
        int numLength = (int)Math.Log10(number) + 1;

        int numberSpare = number;
        int sum = 0;

        for (int i = 0; i < numLength; i++)
        {
            int currentDigit = numberSpare % 10;
            int factorial = 1;

            if (currentDigit == 0) sum++;
            do
            {
                factorial *= currentDigit;
                currentDigit--;
            } while (currentDigit > 0);

            sum += factorial;
            numberSpare = numberSpare / 10;
        }

        //Console.WriteLine(sum);
        if (number == sum) Console.WriteLine("yes");
        else Console.WriteLine("no");
    }
}
