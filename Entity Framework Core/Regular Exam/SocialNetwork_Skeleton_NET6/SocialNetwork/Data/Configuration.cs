using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public class Configuration
    {
        public static string ConnectionString
            => @"Server=KINGCRIMSON\SQLEXPRESS;Database=SocialNetwork_Jul26;Trusted_Connection=True;Encrypt=False;";
    }
}
