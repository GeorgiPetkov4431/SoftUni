using System;

namespace _05._Multiply_Big_Number;

internal class Program
{
    static void Main(string[] args)
    {
        string bigNumber = Console.ReadLine();
        string multiplier = Console.ReadLine();

        string product = MultiplyNumber(bigNumber, multiplier);
        Console.WriteLine(product);
    }

    static string MultiplyNumber(string bigNumber, string multiplierString)
    {
        if (bigNumber == "0" || multiplierString == "0") return "0";

        int carry = 0;
        int multiplier = int.Parse(multiplierString);

        char[] resultChars = new char[bigNumber.Length + 1];

        // Multiply operation
        for (int i = bigNumber.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(bigNumber[i].ToString());
            int product = digit * multiplier + carry;

            resultChars[i + 1] = (char)((product % 10) + '0');
            carry = product / 10;
        }

        if (carry > 0)
        {
            resultChars[0] = (char)(carry + '0');
        }

        return new string(resultChars).TrimStart('\0');
    }
}
