using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Browse(string url)
        {
            bool isValid = url.All(digit => !char.IsDigit(digit));
            //bool isValid = url.All(!char.IsDigit);

            if (!isValid)
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public string Call(string number)
        {
            bool isValid = number.All(digit => char.IsDigit(digit));
            //bool isValid = number.All(IsDigit);

            if (!isValid)
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Calling... {number}";
        }
    }
}
