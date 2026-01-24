namespace EvenLines
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    public class EvenLines
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";

            Console.WriteLine(ProcessLines(inputFilePath));
        }

        /*public static string ProcessLines(string inputFilePath)
        {
            using StreamReader sr = new StreamReader(inputFilePath);

            StringBuilder sb = new StringBuilder();
            char[] replacableChars = { '-', ',', '.', '!', '?' };

            int lineCounter = 0;

            while (true)
            {
                string line = sr.ReadLine();

                if (lineCounter % 2 != 0)
                {
                    lineCounter++;
                    continue;
                }

                if (line == null)
                {
                    break;
                }

                string[] lineArr = line.Split(' ').Reverse().ToArray();

                foreach (string currentWord in lineArr)
                {
                    for (int i = 0; i < currentWord.Length; i++)
                    {
                        if (replacableChars.Contains(currentWord[i]))
                        {
                            sb.Append('@');
                        }
                        else
                        {
                            sb.Append(currentWord[i]);
                        }
                    }

                    sb.Append(' ');
                }

                sb.Append('\n');

                lineCounter++;
            }

            return sb.ToString();
        }*/

        public static string ProcessLines(string inputFilePath)
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                StringBuilder fullText = new StringBuilder();
                int lineCount = 0;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (lineCount % 2 == 0)
                    {
                        line = ReplaceSymbols(line);
                        line = ReverseLine(line);
                        fullText.AppendLine(line);
                    }
                    lineCount++;
                }

                return fullText.ToString();
            }
        }

        private static string ReplaceSymbols(string line)
        {
            StringBuilder newLine = new StringBuilder(line);
            char[] replacableSymbols = { '-', ',', '.', '!', '?' };

            foreach (char symbol in replacableSymbols)
            {
                newLine.Replace(symbol, '@');
            }

            return newLine.ToString();
        }

        private static string ReverseLine(string line)
        {
            string[] lineArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();
            return string.Join(" ", lineArr);
        }
    }
}
