using System;
using System.Collections.Generic;

namespace Common
{
    public class Logger
    {
        private static List<string> InMemoryLog = new List<string>();

        public static void Log(string message) =>
            InMemoryLog.Add(message);

        public static void LogInfo(string message)
        {
            if (Utils.LogLevel == "Info") 
            {
                InMemoryLog.Add(message);
            }
        }

        public static void LogDebug(string message)
        {
            if (Utils.LogLevel == "Debug")
            {
                InMemoryLog.Add(message);
            }
        }

        public static string Dump() => 
            Utils.ConvertToString(InMemoryLog);

        public static void SaveLog(string fileName) => 
            Utils.SaveToFile(fileName, InMemoryLog);
    }
}