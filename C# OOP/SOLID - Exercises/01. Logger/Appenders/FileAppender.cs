using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using LoggerSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Appenders
{
    public class FileAppender : IAppender
    {
        private readonly ILayout layout;
        private readonly ILogFile logFile;

        public FileAppender(ILayout layout, ILogFile logFile)
        {
            this.layout = layout;
            this.logFile = logFile;
        }

        public ILogFile LogFile { get { return this.logFile; } }

        public ReportLevel ReportLevel { get; set; }

        public int MessagesAppended { get; private set; }

        public void Append(string date, ReportLevel reportLevel, string message)
        {
            if (reportLevel >= this.ReportLevel)
            {
                string formattedMessage = this.layout.Format(date, reportLevel, message);

                this.logFile.Write(formattedMessage);

                MessagesAppended++;
            }
        }
    }
}
