using odevSon.Interfaces;
using System;
using ILogger = odevSon.Interfaces.ILogger;

namespace odevSon.Services
{
    public class DatabaseLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Database log: {message}"); // Veritabanına kaydetme simülasyonu
        }
    }
}
