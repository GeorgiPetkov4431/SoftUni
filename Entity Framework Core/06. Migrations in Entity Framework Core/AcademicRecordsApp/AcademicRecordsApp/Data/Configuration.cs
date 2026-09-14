using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicRecordsApp.Data
{
    public class Configuration
    {
        public static string ConnectionString
            => "Server=KINGCRIMSON\\SQLEXPRESS;Database=AcademicRecordsDB;Trusted_Connection=True;Encrypt=False;";
    }
}
