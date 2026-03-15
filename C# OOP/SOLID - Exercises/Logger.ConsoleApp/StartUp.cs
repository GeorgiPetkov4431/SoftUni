using LoggerSystem.ConsoleApp;
using LoggerSystem.Appenders;
using LoggerSystem.Interfaces;
using LoggerSystem.Services;
using LoggerSystem.Core;

namespace LoggerSystem.ConsoleApp
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            //CommandInterpreter interpreter = new CommandInterpreter();
            //interpreter.Run();

            ILayout layout = new HtmlLayout();
            IAppender appender = new FileAppender(layout, new LogFile());
            ILogger logger = new Logger(appender);

            logger.Fatal("3/31/2025 5:23:54 PM", "mscorlib.dll does not respond");
        }
    }
}
