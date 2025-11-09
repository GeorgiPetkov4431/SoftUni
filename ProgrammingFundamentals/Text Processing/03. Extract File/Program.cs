using System;
using System.Linq;

namespace _03._Extract_File;

internal class Program
{
    static void Main(string[] args)
    {
        /*string[] filePath = Console.ReadLine().Split('\\');
        string[] file = filePath[filePath.Length - 1].Split('.');

        Console.WriteLine($"File name: {file[0]}");
        Console.WriteLine($"File extension: {file[1]}");*/

        string filePath = Console.ReadLine();

        int lastSeparatorIndex = filePath.LastIndexOf('\\');
        int extensionIndex = filePath.LastIndexOf('.');

        if (lastSeparatorIndex != -1 && extensionIndex != -1 && extensionIndex > lastSeparatorIndex)
        {
            string fileName = filePath.Substring(lastSeparatorIndex + 1, extensionIndex - lastSeparatorIndex - 1);

            string fileExtension = filePath.Substring(extensionIndex + 1);

            Console.WriteLine($"File name: {fileName}");
            Console.WriteLine($"File extension: {fileExtension}");
        }
    }
}
