using mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            var model = new RegisterBindingModel
            {
                firstName = firstName,
                lastName = lastName,
                email = email,
                cnp = cnp,
                dateOfBirth = DateTime.Now,
                phoneNumber = phoneNumber,
                password = password,
                repeatPassword = repeatPassword,
                doctorId = 1
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var respons = await client.PostAsync(registerUrl, content);
            HttpContent cn = respons.Content;
            Console.WriteLine(cn.ReadAsStringAsync());
            return respons.IsSuccessStatusCode;
            
        }
    }
}
