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
using mobile.ViewModels;
using System.Net.Http.Headers;

namespace mobile.Services
{
    class ApiServices : IApiServices
    {

        HttpClient client;

        public static string BaseAddress =
    Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public static string registerUrl = $"{BaseAddress}/api/users/register";
        public static string loginUrl = $"{BaseAddress}/api/users/login";
        public static string getUnapprovedUsersUrl = $"{BaseAddress}/api/users/getUnapprovedUsers";
        public static string getAboutUsDoctorsUrl = $"{BaseAddress}/api/users/getDoctors";
        public static string getPatientInfoUrl = $"{BaseAddress}/api/users/MyAccount";
        public static string deleteUser = $"{BaseAddress}/api/users/DeletePatient";
        public static string approveUser = $"{BaseAddress}/api/users/ApproveUser";

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
                password = Password,
                
                
            };
            
            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(loginUrl, httpContent);
            var usr = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                JObject userDynamic = JsonConvert.DeserializeObject<dynamic>(usr);

                App.user.token = userDynamic.Value<string>("token");
                App.user.role = userDynamic.Value<int>("role");
                return true;
            }
            return false;
        }

        public async Task<List<PatientModel>> GetUnapprovedPatientsAsync(string token) {

            List<PatientModel> patients = new List<PatientModel>();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getUnapprovedUsersUrl);
            var usr = await response.Content.ReadAsStringAsync();
            JArray userDynamic = JsonConvert.DeserializeObject<dynamic>(usr);
            foreach (JObject p in userDynamic)
            {
                patients.Add(new PatientModel
                {
                    Id = p.Value<int>("id"),
                    FirstName = p.Value<string>("firstName"),
                    LastName = p.Value<string>("lastName"),
                    Email = p.Value<string>("email"),
                    cnp = p.Value<string>("cnp"),
                    Phone = p.Value<string>("phoneNumber")

                }) ;
            }
            return patients;

        }
        public async Task<List<DoctorModel>> GetAboutUsDoctorsAsync()
        {
            List<DoctorModel> doctors = new List<DoctorModel>();
            var response = await client.GetAsync(getAboutUsDoctorsUrl);
            var usr = await response.Content.ReadAsStringAsync();
            JArray userDynamic = JsonConvert.DeserializeObject<dynamic>(usr);

            foreach (JObject d in userDynamic)
            {
                doctors.Add(new DoctorModel
                {
                    Id = d.Value<int>("id"),
                    FirstName = d.Value<string>("firstName"),
                    LastName = d.Value<string>("lastName"),
                    Email = d.Value<string>("email"),
                    Phone = d.Value<string>("phoneNumber"),
                    Description = d.Value<string>("Description"),
                    Image = d.Value<string>("photo")
                });
            }
            return doctors;
        }
        public async Task<dynamic> GetPatientProfileAsync(string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getPatientInfoUrl);
            string usr = await response.Content.ReadAsStringAsync();
            JObject userDynamic = JsonConvert.DeserializeObject<dynamic>(usr);
            return 1;
        }
        public async Task<bool> DeleteUserAsync(string token, int id)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var json = JsonConvert.SerializeObject(id);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.DeleteAsync(deleteUser + "?id=" + id);
            if (response.IsSuccessStatusCode)
                return true;
            else return false;

        }
        public async Task<bool> ApproveUserASync(string token, int id)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var json = JsonConvert.SerializeObject(id);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(approveUser + "?id=" + id, content);
            if (response.IsSuccessStatusCode)
                return true;
            else return false;

        }

    }
}
