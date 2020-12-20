
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class PatientModel : Patient
    {
        // testing the visibility of the details
        public bool IsVisible { get; set; }
        public string FullName
        {
            get
            {

                return FirstName + " " + LastName;
            }

        }
        public ICommand AcceptPatientCommand

        {
            get
            {
                return new Command(() =>
                {
                    App.plm.ApproveUser();
                });
            }
        }
        public ICommand DeletePatientCommand

        {
            get
            {
                return new Command(() =>
                {
                    App.plm.DeleteUser();
                });
            }
        }
        public ICommand DeleteCommandPatientList

        {
            get
            {
                return new Command(() =>
                {
                    App.plm.DeleteUserPatient();
                });
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await App.apiServicesManager.UpdateUserAsync(FirstName, LastName, Phone, Email, App.user.token);
                    if (isSuccess)
                    {
                        Console.WriteLine("cu suces");
                    }
                    else
                    {
                        Console.WriteLine("fara suces");
                    }
                });


            }
        }
    }
}
