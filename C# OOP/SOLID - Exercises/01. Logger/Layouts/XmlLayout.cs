using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.Layouts
{
    public class XmlLayout : ILayout
    {
        public string Format(string date, ReportLevel reportLevel, string message)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<log>");
            sb.AppendLine($"\t<date>{date}</date>");
            sb.AppendLine($"\t<level>{reportLevel}</level>");
            sb.AppendLine($"\t<message>{message}</message>");
            sb.AppendLine("</log>");

            return sb.ToString();
        }
    }
}
