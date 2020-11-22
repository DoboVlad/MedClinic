using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
namespace mobile.Services
{
    class ApiServices
    {
       
        public static string BaseAddress =
    Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public static string registerUrl = $"{BaseAddress}/api/users/register";

       

        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string cnp, DateTime dateOfBirth, string phoneNumber, string password, string repeatPassword, int doctorId)
        {

            //Console.WriteLine(firstName + ' ' + lastName + ' ' + email + ' ' + cnp + ' ' + dateOfBirth.ToString() + ' ' + phoneNumber + ' ' + password + ' ' + repeatPassword + ' ' + doctorId);

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client;
#if DEBUG
            client = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
#else
            client = new HttpClient();
#endif
            var model = new RegisterBindingModel
            {
                firstName = firstName,
                lastName = lastName,
                email = email,
                cnp = cnp,
                dateOfBirth = dateOfBirth,
                phoneNumber = cnp,
                password = password,
                repeatPassword = repeatPassword,
                doctorId = doctorId
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var respons = await client.PostAsync(registerUrl, content);
            return respons.IsSuccessStatusCode;
            
        }
    }
}
