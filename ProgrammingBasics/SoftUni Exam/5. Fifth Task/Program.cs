int targetProfit = int.Parse(Console.ReadLine());

int profit = 0;

while (true)
{
    string serviceType = Console.ReadLine();
    if (serviceType == "closed") break;
    else if (serviceType == "haircut")
    {
        serviceType = Console.ReadLine();
        switch (serviceType) {
            case "mens":
                profit += 15;
                break;
            case "ladies":
                profit += 20;
                break;
            case "kids":
                profit += 10;
                break;
        }
    }
    else if (serviceType == "color")
    {
        serviceType = Console.ReadLine();
        switch (serviceType)
        {
            case "touch up":
                profit += 20;
                break;
            case "full color":
                profit += 30;
                break;
        }
    }
    if (profit >= targetProfit) break;
}

if (profit >= targetProfit)
{
    Console.WriteLine("You have reached your target for the day!");
    Console.WriteLine($"Earned money: {profit}lv.");
}
else
{
    Console.WriteLine($"Target not reached! You need {targetProfit - profit}lv. more.");
    Console.WriteLine($"Earned money: {profit}lv.");
}