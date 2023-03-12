using System;
using System.IO;

namespace Server.Helpers
{
    public static class MyLogger
    {
        private static readonly string LogFilePath = Paths.GetLogsFilePath();

        public static void LogException(string message, Exception ex)
        {
            string logMessage = $"Time: {DateTime.Now.ToString()},  Description: {message},   Exception Message: {ex.Message}.\n";

            using (StreamWriter writer = File.AppendText(LogFilePath))
            {
                writer.WriteLine(logMessage);
            }
        }
    }
}
