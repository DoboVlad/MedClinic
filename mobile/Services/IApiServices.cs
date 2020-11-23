using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services
{
    public interface IApiServices
    {
        Task<bool> RegisterAsync(string firstName, string lastName, string email, string cnp, DateTime dateOfBirth, string phoneNumber, string password, string repeatPassword, int doctorId);
         Task<bool> LoginAsync(string UserName, string Password);

    }
}
