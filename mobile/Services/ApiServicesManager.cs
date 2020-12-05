using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;

namespace mobile.Services
{
   public class ApiServicesManager
    {
        IApiServices apiS;
       public ApiServicesManager(IApiServices apiServices)
        {
            apiS = apiServices;
        }
        public Task<bool> Register(string firstName, string lastName, string email, string cnp, DateTime dateOfBirth, string phoneNumber, string password, string repeatPassword, int doctorId) {

            return apiS.RegisterAsync( firstName,  lastName,  email,  cnp,  dateOfBirth,  phoneNumber,  password,  repeatPassword,  doctorId);
        }
        public async Task<bool> LoginAsync(string UserName, string Password)
        {
            return await apiS.LoginAsync(UserName, Password);
        }
        public async Task<bool> getUnapprovedUsers() {
            return await apiS.GetUnapprovedPatientsAsync();
        }
        public async Task<List<DoctorModel>> GetAboutUsDoctorsAsync() {
            return await apiS.GetAboutUsDoctorsAsync();
        }
        public async Task<dynamic> GetPatientProfileAsync(string token)
        {
            return await apiS.GetPatientProfileAsync(token);
        }
    }
}
