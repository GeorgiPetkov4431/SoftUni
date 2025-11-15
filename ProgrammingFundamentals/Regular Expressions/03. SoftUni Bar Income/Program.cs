using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _03._SoftUni_Bar_Income;

internal class Program
{
    static void Main(string[] args)
    {
        decimal totalIncome = 0m;
        List<Order> orders = new List<Order>();

        string orderPattern = @"%(?<Name>[A-Z][a-z]+)%[^|$%.]*\<(?<Product>\w+)\>[^|$%.]*\|(?<Count>\d+)\|[^|$%.]*?(?<Price>\d+\.\d+|\d+)\$";
        Regex regex = new(orderPattern);

        string command;
        while ((command = Console.ReadLine()) != "end of shift")
        {
            Match match = regex.Match(command);
            if (!match.Success) continue;

            string customerName = match.Groups["Name"].Value;
            string productName = match.Groups["Product"].Value;
            int count = int.Parse(match.Groups["Count"].Value);
            decimal price = decimal.Parse(match.Groups["Price"].Value);

            Order newOrder = new Order(customerName, productName, count, price);
            totalIncome += newOrder.TotalPrice;
            orders.Add(newOrder);
        }

        foreach (Order o in orders)
        {
            Console.WriteLine(o.ToString());
        }

        Console.WriteLine($"Total income: {totalIncome:F2}");
    }
}

class Order
{
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }

    public decimal TotalPrice { get => Count * Price; }

    public Order(string customerName, string productName, int count, decimal price)
    {
        CustomerName = customerName;
        ProductName = productName;
        Count = count;
        Price = price;
    }

    public override string ToString()
    {
        return $"{CustomerName}: {ProductName} - {TotalPrice:F2}";
    }
}
