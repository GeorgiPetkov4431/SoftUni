using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Core.Interfaces;
using Telephony.IO.Interfaces;
using Telephony.Models;
using Telephony.Models.Interfaces;

namespace Telephony.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] phoneNumberList = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] urlList = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

                    writer.WriteLine(callable.Call(phoneNumber));
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (string url in urlList)
            {
                try
                {
                    browsable = new Smartphone();
                    writer.WriteLine(browsable.Browse(url));
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
