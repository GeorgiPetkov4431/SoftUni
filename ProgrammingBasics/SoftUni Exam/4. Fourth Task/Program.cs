int computerNum = int.Parse(Console.ReadLine());

double totalSales = 0;
double totalRating = 0;

for (int i = 0; i < computerNum; i++)
{
    int computerModel = int.Parse(Console.ReadLine());
    int rating = computerModel % 10;
    totalRating += rating;
    computerModel /= 10;
    if (rating == 2) totalSales += 0;
    else if (rating == 3) totalSales = totalSales + computerModel / 2;
    else if (rating == 4) totalSales = totalSales + computerModel * 0.7;
    else if (rating == 5) totalSales = totalSales + computerModel * 0.85;
    else if (rating == 6) totalSales = totalSales + computerModel;
}

Console.WriteLine($"{totalSales:F2}");
Console.WriteLine($"{totalRating / computerNum:F2}");