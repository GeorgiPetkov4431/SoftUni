using System;

namespace _04._Password_Validator;

internal class Program
{
    static void Main(string[] args)
    {
        string password = Console.ReadLine();

        bool sizeCheckPassed = CheckPasswordSize(password);
        bool symbolCheckPassed = CheckPasswordSymbols(password);
        bool digitCountCheckPassed = CheckPasswordDigitCount(password);

        if (!sizeCheckPassed) Console.WriteLine("Password must be between 6 and 10 characters");
        if (!symbolCheckPassed) Console.WriteLine("Password must consist only of letters and digits");
        if (!digitCountCheckPassed) Console.WriteLine("Password must have at least 2 digits");

        if (sizeCheckPassed && symbolCheckPassed && digitCountCheckPassed) Console.WriteLine("Password is valid");
    }

    static bool CheckPasswordSize(string password)
    {
        if (password.Length >= 6 && password.Length <= 10) return true;
        return false;
    }

    static bool CheckPasswordSymbols(string password)
    {
        foreach (char symbol in password)
        {
            if (symbol >= 65 && symbol <= 90 || symbol >= 97 && symbol <= 122 || symbol >= 48 && symbol <= 57) continue;
            else return false;
        }
        return true;
    }

    static bool CheckPasswordDigitCount(string password)
    {
        byte count = 0;
        foreach (char symbol in password)
        {
            if (symbol >= 48 && symbol <= 57) count++;
        }

        if (count < 2) return false;
        return true;
    }
}
