using mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    class RegisterModel
    {
        ApiServices _apiServices= new ApiServices();
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string cnp { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string phoneNumber { get; set; }
        public string password { get; set; }

        public String repeatPassword { get; set; }

        public int doctorId { get; set; }

        public string Message { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                   var isSuccess = await _apiServices.RegisterAsync(firstName, lastName, email, cnp, dateOfBirth, phoneNumber, password, repeatPassword, doctorId);
                    if (isSuccess)
                    {
                        Message = "regiser a functionat";
                    }
                    else
                    {
                        Message = "register nu a functionat";
                    }
                } 
                );
            }
        }
    }
}
