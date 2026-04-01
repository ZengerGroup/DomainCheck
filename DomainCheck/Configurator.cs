using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCheck
{
    internal static class Configurator
    {
        public static string LogPath = ConfigurationManager.AppSettings["LogPath"];
        public static string MailTo = ConfigurationManager.AppSettings["MailTo"];
        public static string MailAccount = ConfigurationManager.AppSettings["MailAccount"];
        public static string MailSecret = ConfigurationManager.AppSettings["MailSecret"];
    }
}
