using System;
using System.IO;
using System.Text;

namespace Playground;

internal class Program
{
    static void Main(string[] args)
    {
        MemoryStream memory = new MemoryStream();

        string text = "Ivaylo Kenov";

        byte[] textBytes = Encoding.UTF8.GetBytes(text);

        memory.Write(textBytes, 0, textBytes.Length);

        memory.Flush();
        // ^ fundamentally redundant, as MemoryStream.Write already writes the byte array into memory
        // hence the name 'MEMORY'Stream.
        // When used by other Streams, .Flush() writes the contents of that stream into memory.

        memory.Seek(0, SeekOrigin.Current);

        byte[] result = new byte[memory.Length];

        memory.Read(result, 0, result.Length);

        Console.WriteLine(Encoding.UTF8.GetString(result, 0, result.Length));
    }
}
