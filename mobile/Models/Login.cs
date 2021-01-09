using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using mobile.Views;
using Xamarin.Forms;

namespace mobile.Models
{
    class Login
    {

        public string email { get; set; }

        public string password { get; set; }

        public bool IsLogged { get; set; } 

        public ICommand LoginCommand {
            get
            {
                return new Command(async () =>
                {
                    IsLogged = await App.apiServicesManager.LoginAsync(email, password);

                    if (IsLogged == true)
                    {
                        if (App.user.role == 1)
                        {

                            await Application.Current.MainPage.Navigation.PushAsync(new TabbedMainPageDoctor());
                        }
                        else if (App.user.Validated == 1)
                            await Application.Current.MainPage.Navigation.PushAsync(new TabbedPageMain());
                        else await Application.Current.MainPage.Navigation.PushAsync(new ValidatePage());
                    }

                });
            } }
   
    }

    
}
