using mobile.Resources;
using mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    class RegisterModel: INotifyPropertyChanged
    {
      
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string cnp { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string phoneNumber { get; set; }
        public string password { get; set; }

        public String repeatPassword { get; set; }

        public int doctorId { get; set; }
        private string message;

        public string Message { 
            get {
                return message;
            }
            set {
                message = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AboutUsModel aboutUsModel { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                   var isSuccess = await App.apiServicesManager.Register(firstName, lastName, email, cnp, dateOfBirth, phoneNumber, password, repeatPassword, doctorId);
                    if (isSuccess)
                    {
                        RegistrationSucceedPage.regMessage = AppResources.RegistrationSucceed;
                    }
                    else
                    {
                        RegistrationSucceedPage.regMessage = AppResources.RegistrationNotSucceed;
                    }
                } 
                );
            }
        }
    }
}
