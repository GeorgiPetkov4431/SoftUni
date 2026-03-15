using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.ConsoleApp
{
    internal class HtmlLayout : ILayout
    {
        public string Format(string date, ReportLevel reportLevel, string message)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Log Report</title>
</head>
<body>
    <ul>
        <li>Date: {date} - {reportLevel} - {message}</li>
    </ul>
</body>
</html>";
        }
    }
}
