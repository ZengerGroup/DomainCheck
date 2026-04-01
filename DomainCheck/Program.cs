using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.WriteLog("Beginning domain check process.", true);
            DataStore Store = new DataStore(args[0]);
            HTMLBuilder Builder = new HTMLBuilder();
            Mailer MailClient = new Mailer();
            for(int i=0; i < Store.Rows.Length; i++)
            {
                Logger.WriteLog("Working on: {0}", false, Store.Rows[i][0]);
                Verifier verifier = new Verifier(Store.Rows[i][0]);
                if (!verifier.AttemptConnection().Result) Builder.AddFail("RESPONSE", Store.Rows[i]);
                else if (verifier.SSLExp != "PASS") Builder.AddFail("SSL", new string[] { Store.Rows[i][0], verifier.SSLExp });
                if (!CheckDate(Store.Rows[i][1])) Builder.AddFail("DOMAIN", new string[] { Store.Rows[i][0], Store.Rows[i][1], Store.Rows[i][2] });
                if (!CheckDate(Store.Rows[i][3])) Builder.AddFail("HOST", new string[] { Store.Rows[i][0], Store.Rows[i][3], Store.Rows[i][4] });
            }
            MailClient.SendMail(Builder.GetBody());
            //Console.ReadLine();
        }
        static bool CheckDate(string expiration)
        {
            if (expiration == "N/A") return true;
            if (DateTime.Parse(expiration) < DateTime.Now.AddDays(60)) return false;
            return true;
        }
    }
}