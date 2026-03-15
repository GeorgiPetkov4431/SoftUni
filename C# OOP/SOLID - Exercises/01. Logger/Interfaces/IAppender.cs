using LoggerSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Interfaces
{
    public interface IAppender
    {
        ReportLevel ReportLevel { get; set; }

        int MessagesAppended { get; }

        void Append(string date, ReportLevel reportLevel, string message);
    }
}
