using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Courses;

internal class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Course> courses = new Dictionary<string, Course>();

        string command;

        while ((command = Console.ReadLine()) != "end")
        {
            string[] commandArgs = command.Split(" : ");

            string courseName = commandArgs[0];
            string studentName = commandArgs[1];

            if (!courses.ContainsKey(courseName))
            {
                Course newCourse = new Course(courseName);
                courses.Add(courseName, newCourse);
            }

            courses[courseName].AddStudent(studentName);
        }

        foreach (KeyValuePair<string, Course> course in courses)
        {
            Console.WriteLine(course.Value);
        }
    }
}

class Course
{
    private string CourseName { get; set; }
    private List<string> StudentNames { get; set; }

    public Course(string courseName)
    {
        CourseName = courseName;
        StudentNames = new List<string>();
    }

    public void AddStudent(string student)
    {
        StudentNames.Add(student);
    }

    public override string ToString()
    {
        string result = $"{CourseName}: {StudentNames.Count}\n";
        
        foreach (string student in StudentNames)
        {
            result += $"-- {student}\n";
        }

        return result.Trim();
    }
}
