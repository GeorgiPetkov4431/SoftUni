using System;

namespace _09._Kamino_Factory;

internal class Program
{
    static void Main(string[] args)
    {
        int sequenceLength = int.Parse(Console.ReadLine());
        int bestSequenceLength = 1;
        int bestSequenceIndex = 0;
        int bestIndex = 1;
        int bestSum = 0;
        int index = 0;
        string[] bestSequence = Array.Empty<string>();

        string sequence;

        while ((sequence = Console.ReadLine()) != "Clone them!")
        {
            string[] sequenceArr = sequence.Split('!', StringSplitOptions.RemoveEmptyEntries);
            if (bestSequence.Length == 0) bestSequence = sequenceArr;

            index++;

            int currentBestSequenceLength = 1;
            int currentBestSequenceIndex = 0;

            int currentSequenceLength = 1;
            int currentSequenceIndex = 0;

            int sum = 0;

            for (int i = 0; i < sequenceArr.Length - 1; i++)
            {
                if (sequenceArr[i] == "1")
                {
                    sum++;
                    if (sequenceArr[i] == sequenceArr[i + 1]) currentSequenceLength++;
                    else
                    {
                        currentSequenceLength = 1;
                        currentSequenceIndex = i + 1;
                    }
                    if (currentSequenceLength > currentBestSequenceLength)
                    {
                        currentBestSequenceIndex = currentSequenceIndex;
                        currentBestSequenceLength = currentSequenceLength;
                    }
                }
                else currentSequenceIndex++;
                if (i == sequenceArr.Length - 2 && sequenceArr[i + 1] == "1") sum++;
            }

            if (currentBestSequenceLength > bestSequenceLength
                || (currentBestSequenceIndex < bestSequenceIndex && currentBestSequenceLength == bestSequenceLength)
                || (currentBestSequenceIndex == bestSequenceIndex && currentBestSequenceLength == bestSequenceLength && sum > bestSum))
            {
                bestSequence = sequenceArr;
                bestSequenceIndex = currentBestSequenceIndex;
                bestSequenceLength = currentBestSequenceLength;
                bestIndex = index;
                bestSum = sum;
            }


        }

        Console.WriteLine($"Best DNA sample {bestIndex} with sum: {bestSum}.");
        Console.WriteLine(string.Join(" ", bestSequence));
    }
}
