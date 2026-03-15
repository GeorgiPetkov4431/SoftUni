using LoggerSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Interfaces
{
    public interface ILayout
    {
        string Format(string date, ReportLevel reportLevel, string message);
    }
}
