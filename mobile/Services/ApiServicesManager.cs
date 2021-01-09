using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mobile.Models;
using mobile.ViewModels;
using Xamarin.Forms;

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
        public async Task<List<PatientModel>> getUnapprovedUsers(string token) {
            return await apiS.GetUnapprovedPatientsAsync(token);
        }
        public async Task<List<PatientModel>> GetApprovedUsers(string token)
        {
            return await apiS.GetApprovedUsers(token);
        }
        public async Task<List<DoctorModel>> GetAboutUsDoctorsAsync() {
            return await apiS.GetAboutUsDoctorsAsync();
        }
        public async Task<dynamic> GetPatientProfileAsync(string token)
        {
            return await apiS.GetPatientProfileAsync(token);
        }
        public async Task<dynamic> GetDoctorProfileAsync(string token)
        {
            return await apiS.GetDoctorProfileAsync(token);
        }
        public async Task<bool> ApproveUserASync(string token, PatientModel patient) {
            return await apiS.ApproveUserASync(token, patient);
        }
        public async Task<bool> DeleteUserAsync(string token, int id)
        {
            return await apiS.DeleteUserAsync(token, id);
        }
        public async Task<bool> UpdateUserAsync(string firstName, string lastName, string phoneNumber, string email, string token)
        {
            return await apiS.UpdateUserAsync(firstName, lastName, phoneNumber, email, token);
        }
        public async Task<bool> UpdateDoctorAsync(string firstName, string lastName, string phoneNumber, string email, string description, ImageSource photo, string token)
        {
            return await apiS.UpdateDoctorAsync(firstName, lastName, phoneNumber, email, description, photo, token);
        }
        public async Task<List<AppointmentModel>> GetApptsAsync(string token) {

          return await apiS.GetApptsAsync(token);
        }
        public async Task<bool> DeleteApptAsync(int id) {
            return await apiS.DeleteApptAsync(id);
        }
        public async Task<List<AppointmentModel>> GetNextApptsAsync(string token) {
            return await apiS.GetNextApptsAsync(token);
        }
        public async Task<List<AppointmentModel>> GetBackApptsAsync(string token) {
            return await apiS.GetBackApptsAsync(token);
        }

        public async Task<List<AppointmentModel>> GetBackPatientApptsAsync(string token)
        {
            return await apiS.GetBackPatientAppointments(token);
        }
        public async Task<List<AppointmentModel>> GetNextPatientApptsAsync(string token)
        {
            return await apiS.GetNextPatientAppointments(token);
        }

        public async Task<Boolean> CreateAppointmentAsync(CreateAppointment createAppointment, string token)
        {
            return await apiS.CreateAppointmentAsync(createAppointment, token);
        }
        public async Task<Boolean> SendEmail(string email)
        {
            return await apiS.SendEmail(email);
        }
        public async Task<Boolean> ResetPassword(ResetPasswordModel resetPwd)
        {
            return await apiS.ResetPassword(resetPwd);
        }

        public async Task<bool> ValidateCode(string code) {
            return await apiS.ValidateCode(code);
        }
    }
}
