using System;

namespace Stealer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Spy spy = new Spy();

            string result = spy.StealFieldInfo("Stealer.Hacker", new string[] { "username", "password" });

            Console.WriteLine(result);
        }
    }
}
