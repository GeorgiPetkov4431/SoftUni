using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _02___Fancy_Barcodes;

internal class Program
{
    static void Main(string[] args)
    {
        string validBarcodeExpression = @"\@\#+[A-Z][A-Za-z0-9]{4,}[A-Z]\@\#+";

        int barcodeCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < barcodeCount; i++)
        {
            string barcode = Console.ReadLine();

            Match match = Regex.Match(barcode, validBarcodeExpression);
            if (!match.Success)
            {
                Console.WriteLine("Invalid barcode");
                continue;
            }

            string productGroup = ConcatenateDigits(match.Value);
            Console.WriteLine($"Product group: {productGroup}");
        }
    }

    static string ConcatenateDigits(string barcode)
    {
        StringBuilder digits = new StringBuilder();
        string digitPattern = @"[0-9]";

        MatchCollection matches = Regex.Matches(barcode, digitPattern);
        foreach (Match match in matches)
        {
            digits.Append(match.Value);
        }

        if (digits.Length == 0) digits.Append("00");

        return digits.ToString();
    }
}
