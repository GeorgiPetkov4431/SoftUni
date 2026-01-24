namespace LineNumbers
{
    using System;
    using System.IO;
    using System.Linq;

    public class LineNumbers
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";
            string outputFilePath = @"..\..\..\output.txt";

            ProcessLines(inputFilePath, outputFilePath);
        }

        public static void ProcessLines(string inputFilePath, string outputFilePath)
        {
            string[] fileRead = File.ReadAllLines(inputFilePath);

            int lineCount = 1;

            for (int i = 0; i < fileRead.Length; i++)
            {
                int[] letterAndPunctuationCount = CountAllLettersAndPunctuation(fileRead[i]);

                int letterCount = letterAndPunctuationCount[0];
                int punctuationMarkCount = letterAndPunctuationCount[1];

                fileRead[i] = $"Line {lineCount}: {fileRead[i]} ({letterCount})({punctuationMarkCount})";

                lineCount++;
            }

            File.WriteAllLines(outputFilePath, fileRead);
        }

        public static int[] CountAllLettersAndPunctuation(string line)
        {
            int letterCount = 0;
            int punctuationCount = 0;
            foreach (char letter in line)
            {
                if (char.IsLetter(letter))
                {
                    letterCount++;
                }
                if (char.IsPunctuation(letter))
                {
                    punctuationCount++;
                }
            }
            return [letterCount, punctuationCount];
        }
    }
}
