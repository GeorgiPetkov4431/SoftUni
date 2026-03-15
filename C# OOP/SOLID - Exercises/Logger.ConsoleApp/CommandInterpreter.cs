using LoggerSystem.Appenders;
using LoggerSystem.Core;
using LoggerSystem.Enums;
using LoggerSystem.Interfaces;
using LoggerSystem.Layouts;
using LoggerSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerSystem.ConsoleApp
{
    public class CommandInterpreter
    {
        public void Run()
        {
            int appenderCount = int.Parse(Console.ReadLine());

            List<IAppender> appenders = new List<IAppender>();

            for (int i = 0; i < appenderCount; i++)
            {
                string[] parts = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string appenderType = parts[0];
                string layoutType = parts[1];

                ReportLevel reportLevel = ReportLevel.Info;

                if (parts.Length == 3)
                {
                    reportLevel = Enum.Parse<ReportLevel>(parts[2], true);
                }

                ILayout layout = CreateLayout(layoutType);
                IAppender appender = CreateAppender(appenderType, layout);

                appender.ReportLevel = reportLevel;
                
                appenders.Add(appender);
            }

            ILogger logger = new Logger(appenders.ToArray());

            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                string[] parts = command.Split('|', StringSplitOptions.RemoveEmptyEntries);

                ReportLevel reportLevel = Enum.Parse<ReportLevel>(parts[0], true);
                string date = parts[1];
                string message = parts[2];

                switch (reportLevel)
                {
                    case ReportLevel.Info:
                        logger.Info(date, message);
                        break;

                    case ReportLevel.Warning:
                        logger.Warning(date, message);
                        break;

                    case ReportLevel.Error:
                        logger.Error(date, message);
                        break;

                    case ReportLevel.Critical:
                        logger.Critical(date, message);
                        break;

                    case ReportLevel.Fatal:
                        logger.Fatal(date, message);
                        break;
                }
            }

            PrintLoggerInfo(appenders);
        }

        private ILayout CreateLayout(string layoutType)
        {
            return layoutType switch
            {
                "SimpleLayout" => new SimpleLayout(),
                "XmlLayout" => new XmlLayout(),
                _ => throw new InvalidOperationException("Invalid layout type")
            };
        }

        private IAppender CreateAppender(string appenderType, ILayout layout)
        {
            return appenderType switch
            {
                "ConsoleAppender" => new ConsoleAppender(layout),
                "FileAppender" => new FileAppender(layout, new LogFile()),
                _ => throw new InvalidOperationException("Invalid appender type")
            };
        }

        public void PrintLoggerInfo(List<IAppender> appenders)
        {
            Console.WriteLine("Logger info");
            
            foreach (var appender in appenders)
            {
                if (appender is FileAppender fileAppender)
                {
                    Console.WriteLine(
                        $"Appender type: {appender.GetType().Name}, " + 
                        $"Layout type: {fileAppender.GetType().Name}, " +
                        $"Report level: {appender.ReportLevel}, " +
                        $"Messages appended: {appender.MessagesAppended}, " +
                        $"File size: {fileAppender.LogFile.Size}");
                }
                else
                {
                    Console.WriteLine(
                        $"Appender type: {appender.GetType().Name}, " +
                        $"Layout type: {appender.GetType().Name}, " +
                        $"Report level: {appender.ReportLevel}, " +
                        $"Messages appended: {appender.MessagesAppended}");
                }
            }
        }
    }
}
