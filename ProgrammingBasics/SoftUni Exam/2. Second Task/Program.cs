int daysOfAbsence = int.Parse(Console.ReadLine());
int foodLeft = int.Parse(Console.ReadLine());
double firstDeer = double.Parse(Console.ReadLine());
double secondDeer = double.Parse(Console.ReadLine());
double thirdDeer = double.Parse(Console.ReadLine());

double totalFoodEaten = daysOfAbsence * (firstDeer + secondDeer + thirdDeer);

if (totalFoodEaten <= foodLeft) Console.WriteLine($"{Math.Floor(foodLeft - totalFoodEaten)} kilos of food left.");
else Console.WriteLine($"{Math.Ceiling(totalFoodEaten - foodLeft)} more kilos of food are needed.");