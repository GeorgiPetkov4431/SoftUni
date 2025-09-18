int firstDigit = int.Parse(Console.ReadLine());
int secondDigit = int.Parse(Console.ReadLine());
int thirdDigit = int.Parse(Console.ReadLine());

for (int firstNum = 1; firstNum <= firstDigit; firstNum++)
{
    for (int secondNum = 1; secondNum <= secondDigit; secondNum++)
    {
        for (int thirdNum = 1; thirdNum <= thirdDigit; thirdNum++)
        {
            bool firstEven = firstNum % 2 == 0;
            bool thirdEven = thirdNum % 2 == 0;
            bool secondPrime = (secondNum == 2 || secondNum == 3 || secondNum == 5 || secondNum == 7);

            if (firstEven && thirdEven && secondPrime) Console.WriteLine($"{firstNum} {secondNum} {thirdNum}");
        }
    }
}