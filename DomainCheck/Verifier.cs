using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DomainCheck
{
    internal class Verifier
    {
        public string SSLExp;
        private string URL;
        private HttpClient Client;
        private HttpResponseMessage Response;
        private HttpClientHandler Handler;
        public Verifier(string url)
        {
            URL = url;
            Handler = new HttpClientHandler();
            Handler.ServerCertificateCustomValidationCallback = CertVerifier;
            Client = new HttpClient(Handler);
        }
        public async Task<bool> AttemptConnection()
        {
            try { Response = await Client.GetAsync(@"https://" + URL); }
            catch { return false; }
            if(Response.StatusCode.ToString() == "OK" || Response.StatusCode.ToString() == "Forbidden") return true;
            else return false;
        }
        private bool CertVerifier(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslErrors)
        {
            if (certificate.NotAfter.Date < DateTime.Now.AddDays(60)) SSLExp = certificate.NotAfter.ToString();
            else SSLExp = "PASS";
            return true;
        }
    }
}
