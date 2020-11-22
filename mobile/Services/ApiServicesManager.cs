using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
