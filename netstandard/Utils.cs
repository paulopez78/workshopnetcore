using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Common
{
    public static class Utils
    {
        public static string LogLevel
        {
            get => ConfigurationManager.AppSettings["LogLevel"] ?? "Info";
        }

        public static string ConvertToString(IEnumerable<string> collection) =>
            string.Join(",", collection);


        public static void SaveToFile(string filename, IEnumerable<string> collection)
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Info"));

            foreach (var logEntry in collection)
            {
                dt.Rows.Add(logEntry);
            }

            var dataSet = new DataSet();
            dataSet.Tables.Add(dt);
            dataSet.WriteXml(filename);
        }
    }
}