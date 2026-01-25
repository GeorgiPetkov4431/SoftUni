namespace ZipAndExtract
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;

    public class ZipAndExtract
    {
        static void Main()
        {
            /*using (StreamWriter sr = new StreamWriter("text.txt"))
            {
                sr.Write("what's up");
            }

            string file = Path.GetFullPath("text.txt");
            Console.WriteLine(file);

            Console.WriteLine();

            string[] files = Directory.GetFiles(".").Select(f => f = Path.GetFileName(f)).ToArray();
            Console.WriteLine(string.Join("\n", files));

            return;*/
            string inputFile = @"..\..\..\copyMe.png";
            string zipArchiveFile = @"..\..\..\archive.zip";
            string extractedFile = @"..\..\..\extracted.png";

            ZipFileToArchive(inputFile, zipArchiveFile);

            var fileNameOnly = Path.GetFileName(inputFile);
            ExtractFileFromArchive(zipArchiveFile, fileNameOnly, extractedFile);
        }

        public static void ZipFileToArchive(string inputFilePath, string zipArchiveFilePath)
        {
            using (ZipArchive zip = ZipFile.Open(zipArchiveFilePath, ZipArchiveMode.Create))
            {
                string fileName = Path.GetFileName(inputFilePath);
                zip.CreateEntryFromFile(inputFilePath, fileName);
            }
        }

        public static void ExtractFileFromArchive(string zipArchiveFilePath, string fileName, string outputFilePath)
        {
            using (ZipArchive archive = ZipFile.Open(zipArchiveFilePath, ZipArchiveMode.Read))
            {
                ZipArchiveEntry entry = archive.GetEntry(fileName);
                entry.ExtractToFile(outputFilePath);
            }
        }
    }
}
