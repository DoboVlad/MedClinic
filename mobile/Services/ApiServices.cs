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
using System.Linq;

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
        public static string getApprovedUsersUrl = $"{BaseAddress}/api/users/getPatients";
        public static string getAboutUsDoctorsUrl = $"{BaseAddress}/api/users/getDoctors";
        public static string getPatientInfoUrl = $"{BaseAddress}/api/users/MyAccount";
        public static string getDoctorInfoUrl = $"{BaseAddress}/api/users/MyAccountMedic";
        public static string deleteUser = $"{BaseAddress}/api/users/DeletePatient";
        public static string approveUser = $"{BaseAddress}/api/users/ApproveUser";
        public static string updatePatient = $"{BaseAddress}/api/users/updateUser"; 
        public static string updateDoctor = $"{BaseAddress}/api/users/updateMedic";
        public static string getAppts = $"{BaseAddress}/api/appointments/myAppointments";
        public static string getBackAppts = $"{BaseAddress}/api/appointments/historyAppointmentsByMedic";
        public static string getNextAppts = $"{BaseAddress}/api/appointments/nextAppointmentsByMedic";
        public static string deleteAppt = $"{BaseAddress}/api/appointments/delete";
        public static string getAllAppts = $"{BaseAddress}/api/appointments/allDoctorApp/";
        public static string createAppointmentUrl = $"{BaseAddress}/api/appointments/createApp";
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
                App.user.id = userDynamic.Value<int>("id");
                return true;
            }
            return false;
        }
        public async Task<Boolean> CreateAppointmentAsync (CreateAppointment createAppointment, string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var jsonApp = JsonConvert.SerializeObject(createAppointment);
            HttpContent content = new StringContent(jsonApp);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var respons = await client.PostAsync(createAppointmentUrl, content);
            return true;
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
        public async Task<List<PatientModel>> GetApprovedUsers(string token)
        {

            List<PatientModel> patients = new List<PatientModel>();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getApprovedUsersUrl);
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
                    Phone = p.Value<string>("phoneNumber"),
                    age = p.Value<int>("age")

                });
            }
            return patients;
        }
        public async Task<List<DoctorModel>> GetAboutUsDoctorsAsync()
        {
            List<DoctorModel> doctors = new List<DoctorModel>();
            var response = await client.GetAsync(getAboutUsDoctorsUrl);
            if (response.IsSuccessStatusCode)
            {
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
                foreach (DoctorModel dm in doctors)
                {
                    int start = dm.Image.IndexOf("img");

                    dm.Image = dm.Image.Substring(start + 4);

                }
            }
            return doctors;
        }
        public async Task<dynamic> GetPatientProfileAsync(string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getPatientInfoUrl);
            var content = await response.Content.ReadAsStringAsync();
            JObject userData =  JsonConvert.DeserializeObject<dynamic>(content.ToString());
            JObject o = (JObject)JObject.Parse(content)["doctor"];
            PatientModel patient = new PatientModel
            {
                Id = userData.Value<int>("id"),
                FirstName = userData.Value<string>("firstName"),
                LastName = userData.Value<string>("lastName"),
                Email = userData.Value<string>("email"),
                Phone = userData.Value<string>("phoneNumber"),
                cnp = userData.Value<string>("cnp"),
                age = userData.Value<int>("age"),
                Doctor = new DoctorModel()
                {
                    FirstName = o.Value<string>("firstName"),
                    LastName = o.Value<string>("lastName"),
                    Phone = o.Value<string>("phoneNumber")
                }
            };
            return patient;
        }
        public async Task<dynamic> GetDoctorProfileAsync(string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getDoctorInfoUrl);
            var content = await response.Content.ReadAsStringAsync();
            JObject userData = JsonConvert.DeserializeObject<dynamic>(content.ToString());
            DoctorModel doctor = new DoctorModel
            {
                Id = userData.Value<int>("id"),
                FirstName = userData.Value<string>("firstName"),
                LastName = userData.Value<string>("lastName"),
                Email = userData.Value<string>("email"),
                Phone = userData.Value<string>("phoneNumber"),
                Description = userData.Value<string>("description")
            };
            return doctor;
        }


        public async Task<bool> DeleteUserAsync(string token, int id)
         {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.DeleteAsync(deleteUser + "?id=" + id);
            if (response.IsSuccessStatusCode)
                return true;
            else return false;

        }
        public async Task<bool> DeleteApptAsync( int id)
        {
        
            var response = await client.DeleteAsync(deleteAppt + "/" + id);
            if (response.IsSuccessStatusCode)
                return true;
            else return false;

        }
        public async Task<bool> ApproveUserASync(string token, PatientModel patient)
        {
           
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var json = JsonConvert.SerializeObject(patient.Id);
            HttpContent content = new StringContent(json);
            
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(approveUser + "?id=" + patient.Id, content);
            if (response.IsSuccessStatusCode)
                return true;
            else
            {
                return false;
                
            }

            }
        public async Task<bool> UpdateUserAsync(string firstName, string lastName, string phoneNumber, string email, string token)
        {
            UpdatePatient updateModel = new UpdatePatient
            {
                firstName = firstName,
                lastName = lastName,
                email= email,
                phoneNumber = phoneNumber
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var jsonPatient = JsonConvert.SerializeObject(updateModel);
            HttpContent content = new StringContent(jsonPatient);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var respons = await client.PutAsync(updatePatient, content);
            return respons.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateDoctorAsync(string firstName, string lastName, string phoneNumber, string email, string description, ImageSource photo, string token)
        {
            UpdateDoctor updateModel = new UpdateDoctor
            {
                firstName = firstName,
                lastName = lastName,
                email = email,
                phoneNumber = phoneNumber,
                description = description
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var jsonDoctor = JsonConvert.SerializeObject(updateModel);
            HttpContent content = new StringContent(jsonDoctor);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var respons = await client.PutAsync(updateDoctor, content);
            return respons.IsSuccessStatusCode;
        }
        public async Task<List<AppointmentModel>> GetApptsAsync(string token) {
            List<AppointmentModel> list = new List<AppointmentModel>();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getAppts);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JArray userData = JsonConvert.DeserializeObject<dynamic>(content.ToString());
                foreach (JObject obj in userData)
                {


                    JObject user = obj.Value<JObject>("user");
                    User usr = JsonConvert.DeserializeObject<User>(user.ToString());
                    list.Add(new AppointmentModel
                    {
                        Id = obj.Value<int>("id"),
                        Hour = obj.Value<string>("hour"),
                        Date = obj.Value<DateTime>("date"),
                        Patient = usr

                    });

                    foreach (AppointmentModel am in list)
                    {
                        if (am.Date < DateTime.Now)
                        {
                            am.Status = "Inactive";
                            am.StatusColor = Color.Red;
                        }
                    }

                        
                }
            }
                return list;
           
        }
        public async Task<List<AppointmentModel>> GetNextApptsAsync(string token)
        {
            List<AppointmentModel> list = new List<AppointmentModel>();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getNextAppts);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JArray userData = JsonConvert.DeserializeObject<dynamic>(content.ToString());
                foreach (JObject obj in userData)
                {


                    JObject user = obj.Value<JObject>("user");
                    User usr = JsonConvert.DeserializeObject<User>(user.ToString());
                    list.Add(new AppointmentModel
                    {
                        Id = obj.Value<int>("id"),
                        Hour = obj.Value<string>("hour"),
                        Date = obj.Value<DateTime>("date"),
                        Patient = usr

                    });

                    foreach (AppointmentModel am in list)
                    {
                        if (am.Date < DateTime.Now)
                        {
                            am.Status = "Inactive";
                            am.StatusColor = Color.Red;
                        }
                    }


                }
            }
            return list;

        }
        public async Task<List<AppointmentModel>> GetBackApptsAsync(string token)
        {
            List<AppointmentModel> list = new List<AppointmentModel>();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await client.GetAsync(getBackAppts);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JArray userData = JsonConvert.DeserializeObject<dynamic>(content.ToString());
                foreach (JObject obj in userData)
                {


                    JObject user = obj.Value<JObject>("user");
                    User usr = JsonConvert.DeserializeObject<User>(user.ToString());
                    list.Add(new AppointmentModel
                    {
                        Id = obj.Value<int>("id"),
                        Hour = obj.Value<string>("hour"),
                        Date = obj.Value<DateTime>("date"),
                        Patient = usr

                    });

                    foreach (AppointmentModel am in list)
                    {
                        if (am.Date < DateTime.Now)
                        {
                            am.Status = "Inactive";
                            am.StatusColor = Color.Red;
                        }
                    }


                }
            }
            return list;

        }
     
    }
}
