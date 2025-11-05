using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Student_Academy;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Student> students = new Dictionary<string, Student>();

        int commandNum = int.Parse(Console.ReadLine());

        for (int i =  0; i < commandNum; i++)
        {
            string studentName = Console.ReadLine();
            decimal grade = decimal.Parse(Console.ReadLine());

            if (!students.ContainsKey(studentName))
            {
                Student newStudent = new Student(studentName);
                students.Add(studentName, newStudent);
            }

            students[studentName].AddGrade(grade);
        }

        //students = students.Where(studentPair => studentPair.Value.GradeAverage >= 4.5m).ToDictionary();
        List<Student> filteredStudents = students.Values.Where(studentPair => studentPair.GradeAverage >= 4.5m).ToList();

        foreach (Student student in filteredStudents)
        {
            Console.WriteLine(student);
        }
    }
}

class Student
{
    private string Name { get; set; }
    private List<decimal> Grades { get; set; }
    public Student(string name)
    {
        Name = name;
        Grades = new List<decimal>();
    }

    public void AddGrade(decimal grade)
    {
        Grades.Add(grade);
    }

    public decimal GradeAverage => Grades.Average();

    public override string ToString()
    {
        return $"{Name} -> {Grades.Average():F2}";
    }
}