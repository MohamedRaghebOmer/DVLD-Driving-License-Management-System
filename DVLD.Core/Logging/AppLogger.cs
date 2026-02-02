using System;
using System.IO;

namespace DVLD.Core.Logging
{
    public static class AppLogger
    {
        private static readonly string logFilePath = "logs.txt";

        public static void LogError(string message, Exception ex = null)
        {
            using (var writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} | ERROR | {message}");
                if (ex != null)
                {
                    writer.WriteLine($"Exception: {ex.GetType().Name}: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                        writer.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
            }
        }
    }
}

