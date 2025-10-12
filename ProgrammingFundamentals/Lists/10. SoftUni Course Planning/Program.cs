using System;
using System.Collections.Generic;
using System.Linq;
namespace _10._SoftUni_Course_Planning;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> courseList = Console.ReadLine().Split(", ").ToList();

        string command;

        while ((command = Console.ReadLine()) != "course start")
        {
            string[] commandArgs = command.Split(':');

            switch (commandArgs[0])
            {
                case "Add":
                    AddLesson(courseList, commandArgs[1]);
                    break;
                case "Insert":
                    InsertLesson(courseList, commandArgs[1], int.Parse(commandArgs[2]));
                    break;
                case "Remove":
                    RemoveLesson(courseList, commandArgs[1]);
                    break;
                case "Swap":
                    SwapLessons(courseList, commandArgs[1], commandArgs[2]);
                    break;
                case "Exercise":
                    InsertExercise(courseList, commandArgs[1]);
                    break;
            }
        }

        Console.WriteLine(PrintCourseList(courseList));
    }

    static void AddLesson(List<string> courseList, string lessonTitle)
    {
        if (!courseList.Contains(lessonTitle)) courseList.Add(lessonTitle);
    }

    static void InsertLesson(List<string> courseList, string lessonTitle, int index)
    {
        if (!courseList.Contains(lessonTitle)) courseList.Insert(index, lessonTitle);
    }

    static void RemoveLesson(List<string> courseList, string lessonTitle)
    {
        courseList.Remove(lessonTitle);
        string exerciseTitle = $"{lessonTitle}-Exercise";
        courseList.Remove(exerciseTitle);
    }

    static void SwapLessons(List<string> courseList, string firstTitle, string secondTitle)
    {
        if (courseList.Contains(firstTitle) && courseList.Contains(secondTitle))
        {
            int firstTitleIndex = courseList.FindIndex(x => x == firstTitle);
            int secondTitleIndex = courseList.FindIndex(x => x == secondTitle);

            string temp = courseList[firstTitleIndex];
            courseList[firstTitleIndex] = courseList[secondTitleIndex];
            courseList[secondTitleIndex] = temp;

            SwapExercise(courseList, firstTitle);
            SwapExercise(courseList, secondTitle);
        }
    }

    static void SwapExercise(List<string> courseList, string lessonTitle)
    {
        string exerciseTitle = $"{lessonTitle}-Exercise";
        if (courseList.Contains(exerciseTitle))
        {
            RemoveLesson(courseList, exerciseTitle);
            InsertExercise(courseList, lessonTitle);
        }
    }

    static void InsertExercise(List<string> courseList, string lessonTitle)
    {
        AddLesson(courseList, lessonTitle);

        string exerciseTitle = $"{lessonTitle}-Exercise";
        if (!courseList.Contains(exerciseTitle)) courseList.Insert(courseList.IndexOf(lessonTitle) + 1, exerciseTitle);
    }

    static string PrintCourseList(List<string> courseList)
    {
        string result = string.Empty;
        for (int i = 0; i < courseList.Count; i++)
        {
            result += $"{i + 1}.{courseList[i]}\n";
        }
        return result;
    }
}
