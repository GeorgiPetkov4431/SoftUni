namespace CopyBinaryFile
{
    using System;
    using System.IO;
    public class CopyBinaryFile
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\copyMe.png";
            string outputFilePath = @"..\..\..\copyMe-copy.png";

            CopyFile(inputFilePath, outputFilePath);
        }

        public static void CopyFile(string inputFilePath, string outputFilePath)
        {
            using FileStream originalFile = new FileStream(inputFilePath, FileMode.Open);
            using FileStream newFile = new FileStream(outputFilePath, FileMode.Create);

            ClearFile(newFile);

            byte[] buffer = new byte[4096];

            int bytesRead;

            while ((bytesRead = originalFile.Read(buffer)) > 0)
            {
                newFile.Write(buffer, 0, bytesRead);
            }
        }

        public static void ClearFile(FileStream fileStream)
        {
            int n = 5;
            byte[] buffer = new byte[n];
            int readCount = fileStream.Read(buffer, 0, n);
            while (readCount > 0)
            {
                fileStream.Write(new byte[n], 0, readCount);

                readCount = fileStream.Read(buffer, 0, n);
            }
        }
    }
}
