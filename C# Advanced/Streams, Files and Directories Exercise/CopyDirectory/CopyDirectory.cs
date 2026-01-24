namespace CopyDirectory
{
    using System;
    using System.IO;

    public class CopyDirectory
    {
        static void Main()
        {
            string inputPath =  @$"{Console.ReadLine()}";
            string outputPath = @$"{Console.ReadLine()}";

            CopyAllFiles(inputPath, outputPath);
        }

        public static void CopyAllFiles(string inputPath, string outputPath)
        {
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath);
            }

            Directory.CreateDirectory(outputPath);
            
            string[] filePaths = Directory.GetFiles(inputPath);

            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);

                string destinationFileName = Path.Combine(outputPath, fileName);

                File.Copy(filePath, destinationFileName);
            }
        }
    }
}
