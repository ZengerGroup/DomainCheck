using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCheck
{
    internal class HTMLBuilder
    {
        private List<string[]> ResponseFails;
        private List<string[]> SSLFails;
        private List<string[]> DomainFails;
        private List<string[]> HostFails;
        private string EmailBody;
        private int AlertCount { get { return ResponseFails.Count + SSLFails.Count + DomainFails.Count + HostFails.Count; } }
        public HTMLBuilder()
        {
            ResponseFails = new List<string[]>();
            SSLFails = new List<string[]>();
            DomainFails = new List<string[]>();
            HostFails = new List<string[]>();
            EmailBody = "<!DOCTYPE html><style>table,td{border: solid 2px black; padding: 2px; text-align: center;}</style><body>";
        }
        public void AddFail(string type, string[] data)
        {
            switch (type)
            {
                case "RESPONSE":
                    ResponseFails.Add(data);
                    break;
                case "SSL":
                    SSLFails.Add(data);
                    break;
                case "DOMAIN":
                    DomainFails.Add(data);
                    break;
                case "HOST":
                    HostFails.Add(data);
                    break;
            }
        }
        public string GetBody()
        {
            EmailBody += String.Format("<h1>There {0} {1} alert{2}this month.</h1>", (AlertCount != 0 && AlertCount > 1) ? "are" : "is", AlertCount, (AlertCount != 0 && AlertCount > 1) ? "s ": " ");
            EmailBody += AddTables();
            return EmailBody;
        }
        private string AddTables()
        {
            string tables = "<h2>No Response:</h2>";
            tables += BuildTable(ResponseFails);
            tables += "<h2>SSL Expiring:</h2>";
            tables += BuildTable(SSLFails);
            tables += "<h2>Domain Expiring:</h2>";
            tables += BuildTable(DomainFails);
            tables += "<h2>Host Expiring:</h2>";
            tables += BuildTable(HostFails);
            return tables;
        }
        private string BuildRow(string[] row)
        {
            string parsedRow = "";
            for(int i = 0; i < row.Length; i++)
            {
                parsedRow += String.Format("<td>{0}</td>", row[i]);
            }
            return parsedRow;
        }
        private string BuildTable(List<string[]> list)
        {
            if (list.Count == 0) return "<p>No Alerts to report.</p>";
            string output = "<table>";
            for(int i = 0; i < list.Count; i++)
            {
                output += String.Format("<tr>{0}</tr>", BuildRow(list[i]));
            }
            output += "</table>";
            return output;
        }
    }
}
