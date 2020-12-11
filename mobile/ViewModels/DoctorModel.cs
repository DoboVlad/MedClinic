using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class DoctorModel : Doctor
    {
        public string FullName
        {
            get
            {

                return "Dr. " + FirstName + " " + LastName;
            }

        }
        public ICommand UpdateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await App.apiServicesManager.UpdateDoctorAsync(FirstName, LastName, Phone, Email, Description, Image, App.user.token);
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
