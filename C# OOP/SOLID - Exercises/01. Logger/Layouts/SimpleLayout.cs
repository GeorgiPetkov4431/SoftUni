using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Layouts
{
    public class SimpleLayout : ILayout
    {
        public string Format(string date, ReportLevel reportLevel, string message)
        {
            return $"{date} - {reportLevel.ToString()} - {message}";
        }
    }
}
