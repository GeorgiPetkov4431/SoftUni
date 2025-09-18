int peopleCount = int.Parse(Console.ReadLine());
int nights = int.Parse(Console.ReadLine());
int transportCards =  int.Parse(Console.ReadLine());
int museumTickets = int.Parse(Console.ReadLine());

double sum = (nights * 20 + transportCards * 1.6 + museumTickets * 6) * peopleCount;
sum *= 1.25;

Console.WriteLine($"{sum:F2}");