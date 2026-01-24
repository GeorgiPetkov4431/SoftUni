namespace DirectoryTraversal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class DirectoryTraversal
    {
        static void Main()
        {
            //Console.WriteLine(System.Environment.GetEnvironmentVariable("USERPROFILE"));
            
            string path = Console.ReadLine();
            string reportFileName = @"\report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            Dictionary<string, List<FileInfo>> fileExtensions = new Dictionary<string, List<FileInfo>>();
            string[] filePaths = Directory.GetFiles(inputFolderPath);

            foreach (var file in filePaths)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (!fileExtensions.ContainsKey(fileInfo.Extension))
                {
                    fileExtensions.Add(fileInfo.Extension, new List<FileInfo>());
                }

                fileExtensions[fileInfo.Extension].Add(fileInfo);
            }

            fileExtensions = fileExtensions.OrderByDescending(f => f.Value.Count)
                .ThenBy(f => f.Key)
                .ToDictionary(f => f.Key, f => f.Value);

            StringBuilder result = new StringBuilder();

            foreach (var (extension, fileInfos) in fileExtensions.OrderBy(f => f.Key))
            {
                result.AppendLine($"{extension}");

                foreach (var fileInfo in fileInfos.OrderByDescending(f => f.Length))
                {
                    result.AppendLine($"--{fileInfo.Name} - {(double)fileInfo.Length / 1024:f3}");
                }
            }

            return result.ToString().TrimEnd();
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + reportFileName;
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(textContent);
            }
        }
    }
}
