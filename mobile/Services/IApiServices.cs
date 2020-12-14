using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;
using Xamarin.Forms;

namespace mobile.Services
{
    public interface IApiServices
    {
        Task<bool> RegisterAsync(string firstName, string lastName, string email, string cnp, DateTime dateOfBirth, string phoneNumber, string password, string repeatPassword, int doctorId);
        Task<bool> LoginAsync(string UserName, string Password);
        public Task<List<PatientModel>> GetUnapprovedPatientsAsync(string token);
        public Task<List<PatientModel>> getApprovedUsers(string token);
        public  Task<List<DoctorModel>> GetAboutUsDoctorsAsync();
        public Task<dynamic> GetPatientProfileAsync(string token);
        public Task<dynamic> GetDoctorProfileAsync(string token);
        public  Task<bool> DeleteUserAsync(string token, int id);
        public  Task<bool> ApproveUserASync(string token, PatientModel patient);
        public Task<bool> UpdateUserAsync(string firstName, string lastName, string phoneNumber, string email, string token);
        public Task<bool> UpdateDoctorAsync(string firstName, string lastName, string phoneNumber, string email, string description, ImageSource photo, string token);
        public Task<List<AppointmentModel>> GetApptsAsync( string token);
    }
}
