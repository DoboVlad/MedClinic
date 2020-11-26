using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Microsoft.CSharp.RuntimeBinder;
namespace mobile.Services
{
    class ApiServices : IApiServices
    {

        HttpClient client;

        public static string BaseAddress =
    Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public static string registerUrl = $"{BaseAddress}/api/users/register";
        public static string loginUrl = $"{BaseAddress}/api/users/login";

        // Pass the handler to httpclient(from you are calling api) only in debug mode we have to pass the ssl, in release we dont
        public ApiServices()
        {
#if DEBUG
            client = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
#else
         client = new HttpClient();
#endif
        }
        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string cnp, DateTime dateOfBirth, string phoneNumber, string password, string repeatPassword, int doctorId)
        {

            //Console.WriteLine(firstName + ' ' + lastName + ' ' + email + ' ' + cnp + ' ' + dateOfBirth.ToString() + ' ' + phoneNumber + ' ' + password + ' ' + repeatPassword + ' ' + doctorId);

            
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
        public async Task<bool> LoginAsync(string UserName, string Password) 
        {
            var model = new Login
            {

                email = UserName,
                password = Password
                
            };
            
            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(loginUrl, httpContent);
            var usr = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                JObject userDynamic = JsonConvert.DeserializeObject<dynamic>(usr);
                App.user.id = userDynamic.Value<int>("id");
                App.user.email = userDynamic.Value<string>("email");
                App.user.firstName = userDynamic.Value<string>("firstName");
                App.user.lastName = userDynamic.Value<string>("lastName");
                App.user.role = userDynamic.Value<int>("role");
                App.user.token = userDynamic.Value<string>("token");
                App.user.doctorId = userDynamic.Value<int>("doctorId");
                return true;
            }
            return false;
        }
    }
}
