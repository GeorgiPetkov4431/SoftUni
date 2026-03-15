using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using LoggerSystem.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Appenders
{
    public class ConsoleAppender : IAppender
    {
        private readonly ILayout layout;

        public ConsoleAppender(ILayout layout)
        {
            this.layout = layout;
        }

        public ReportLevel ReportLevel { get; set; }

        public int MessagesAppended { get; private set; }

        public void Append(string date, ReportLevel reportLevel, string message)
        {
            if (reportLevel >= this.ReportLevel)
            {
                string formattedMessage = this.layout.Format(date, reportLevel, message);
                Console.WriteLine(formattedMessage);
                
                MessagesAppended++;
            }
        }
    }
}
