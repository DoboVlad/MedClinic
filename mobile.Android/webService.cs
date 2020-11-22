using System.Net.Http;
using mobile.Droid;
using Xamarin.Forms;
using mobile.Services;

[assembly: Dependency(typeof(HttpClientHandlerService))]
namespace mobile.Droid
{// used to pass over ssl
    public class HttpClientHandlerService : IHttpClientHandlerService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}