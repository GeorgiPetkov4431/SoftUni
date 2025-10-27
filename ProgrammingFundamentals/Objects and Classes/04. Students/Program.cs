using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Students;

internal class Program
{
    static void Main(string[] args)
    {
        List<Student> students = new List<Student>();

        int studentNum = int.Parse(Console.ReadLine());

        for (int i = 0; i < studentNum; i++)
        {
            string[] studentInfo = Console.ReadLine().Split();

            students.Add(new Student(studentInfo[0], studentInfo[1], double.Parse(studentInfo[2])));
        }

        students = students.OrderByDescending(x => x.Grade).ToList();

        Console.WriteLine(string.Join("\n", students));
    }
}


class Student
{
    public Student(string firstName, string lastName, double grade)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Grade = grade;
    }

    public string FirstName;
    public string LastName;
    public double Grade;

    public override string ToString()
    {
        return $"{FirstName} {LastName}: {Grade:F2}";
    }
}