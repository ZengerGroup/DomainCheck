using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace DomainCheck
{
    /// <summary>
    /// Reads a CSV and stores the data in rows in an array.
    /// Format:
    /// 0: Domain Name, 1: Domain Name Expiration, 2: Registrar, 3: Hosting Expiration(Or N/A), 4: Host(Or N/A), 5: Target(IP or redirect URL)
    /// </summary>
    internal class DataStore
    {
        public string[][] Rows;
        public DataStore(string CSVPath)
        {
            if (!File.Exists(CSVPath)) Environment.Exit(1);
            Rows = ReadCSV(CSVPath);
        }
        private TextFieldParser InitParser(string CSVPath)
        {
            TextFieldParser parser = new TextFieldParser(CSVPath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            return parser;
        }
        private string[][] ReadCSV(string CSVPath)
        {
            List<string[]> rows = new List<string[]>();
            TextFieldParser parser = InitParser(CSVPath);
            parser.ReadLine();
            while (!parser.EndOfData) rows.Add(parser.ReadFields());
            parser.Close();
            return rows.ToArray();
        }
    }
}
