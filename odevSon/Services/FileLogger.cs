using odevSon.Interfaces;
using System.IO;
using ILogger = odevSon.Interfaces.ILogger;


namespace odevSon.Services
{
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            var logPath = "logs.txt";
            File.AppendAllText(logPath, $"{DateTime.UtcNow}: {message}{Environment.NewLine}");
        }
    }
}
