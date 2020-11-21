using mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services
{
    class ApiServices
    {
        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string cnp, DateTime dateOfBirth, string phoneNumber, string password, string repeatPassword, int doctorId)
        {
            var client = new System.Net.Http.HttpClient();
            var model = new RegisterBindingModel
            {
                firstName = firstName,
                lastName = lastName,
                email = email,
                cnp = cnp,
                dateOfBirth = dateOfBirth,
                phoneNumber = phoneNumber,
                password = password,
                repeatPassword = repeatPassword,
                doctorId = doctorId
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            //var respons = await client.PostAsync("https://localhost:5001/api/users/register", content);
            //return respons.IsSuccessStatusCode;
            return true;
        }
    }
}
