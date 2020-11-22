using System.Net.Http;

namespace mobile.Services
{

    // used for passing over ssl, also refering a class in droid 
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}