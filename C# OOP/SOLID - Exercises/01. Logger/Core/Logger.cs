using LoggerSystem.Appenders;
using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using LoggerSystem.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Core
{
    public class Logger : ILogger
    {
        private readonly ICollection<IAppender> appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }

        public Logger(ICollection<IAppender> appenders)
        {
            this.appenders = appenders;
        }

        public void Critical(string date, string message)
        {
            AppendAll(date, ReportLevel.Critical, message);
        }

        public void Error(string date, string message)
        {
            AppendAll(date, ReportLevel.Error, message);
        }

        public void Fatal(string date, string message)
        {
            AppendAll(date, ReportLevel.Fatal, message);
        }

        public void Info(string date, string message)
        {
            AppendAll(date, ReportLevel.Info, message);
        }

        public void Warning(string date, string message)
        {
            AppendAll(date, ReportLevel.Warning, message);
        }

        private void AppendAll(string date, ReportLevel reportLevel, string message)
        {
            foreach (var appender in this.appenders)
            {
                if (reportLevel >= appender.ReportLevel)
                {
                    appender.Append(date, reportLevel, message);
                }
            }
        }
    }
}
