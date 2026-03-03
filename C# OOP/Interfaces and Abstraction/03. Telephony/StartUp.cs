using System;
using Telephony.Models;
using Telephony.Models.Interfaces;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] phoneNumberList = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] urlList = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            ICallable callable;
            IBrowsable browsable;
            
            foreach (string phoneNumber in phoneNumberList)
            {
                try
                {
                    if (phoneNumber.Length == 7)
                    {
                        callable = new StationaryPhone();
                    }
                    else
                    {
                        callable = new Smartphone();
                    }

                    Console.WriteLine(callable.Call(phoneNumber));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (string url in urlList)
            {
                try
                {
                    browsable = new Smartphone();
                    Console.WriteLine(browsable.Browse(url));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
