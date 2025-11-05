using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Company_Users;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Company> companies = new Dictionary<string, Company>();

        string command;

        while ((command = Console.ReadLine()) != "End")
        {
            string[] commandArgs = command.Split(" -> ");

            if (!companies.ContainsKey(commandArgs[0]))
            {
                Company newCompany = new Company(commandArgs[0]);
                companies.Add(commandArgs[0], newCompany);
            }

            companies[commandArgs[0]].AddEmployee(commandArgs[1]);
        }

        foreach (KeyValuePair<string, Company> company in companies)
        {
            Console.WriteLine(company.Value);
        }
    }
}

class Company
{
    private string CompanyName { get; set; }
    private List<string> Employees { get; set; }
    public Company(string companyName)
    {
        CompanyName = companyName;
        Employees = new List<string>();
    }

    public void AddEmployee (string employee)
    {
        if (!Employees.Contains(employee)) Employees.Add(employee);
    }

    public override string ToString()
    {
        string result = $"{CompanyName}\n";

        foreach (string employee in Employees)
        {
            result += $"-- {employee}\n";
        }

        return result.Trim();
    }
}