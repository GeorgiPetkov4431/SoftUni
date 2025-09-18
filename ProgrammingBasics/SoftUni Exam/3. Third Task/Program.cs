int daysOfStay = int.Parse(Console.ReadLine());
daysOfStay--;
string accommodationType =  Console.ReadLine();
string rating = Console.ReadLine();

double cost = 0.0;

if (daysOfStay < 10)
{
    switch (accommodationType) {
        case "room for one person":
            cost = daysOfStay * 18.00;
            break;
        case "apartment":
            cost = daysOfStay * 25.00;
            cost *= 0.7;
            break;
        case "president apartment":
            cost = daysOfStay * 35.00;
            cost *= 0.9;
            break;
    }

}
else if (daysOfStay >= 10 && daysOfStay <= 15)
{
    switch (accommodationType)
    {
        case "room for one person":
            cost = daysOfStay * 18.00;
            break;
        case "apartment":
            cost = daysOfStay * 25.00;
            cost *= 0.65;
            break;
        case "president apartment":
            cost = daysOfStay * 35.00;
            cost *= 0.85;
            break;
    }
}
else if (daysOfStay > 15)
{
    switch (accommodationType)
    {
        case "room for one person":
            cost = daysOfStay * 18.00;
            break;
        case "apartment":
            cost = daysOfStay * 25.00;
            cost *= 0.5;
            break;
        case "president apartment":
            cost = daysOfStay * 35.00;
            cost *= 0.8;
            break;
    }
}

switch (rating) {
    case "positive":
        cost *= 1.25;
        break;
    case "negative":
        cost *= 0.9;
        break;
}

Console.WriteLine($"{cost:F2}");