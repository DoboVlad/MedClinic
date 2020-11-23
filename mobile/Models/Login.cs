using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.Models
{
    class Login
    {

        public string email { get; set; }

        public string password { get; set; }

        public bool IsLogged { get; set; } = false;
        public ICommand LoginCommand {
            get
            {
                return new Command(async () =>
                {
                    IsLogged = await App.apiServicesManager.LoginAsync(email, password);

                });
            } }
    }

    
}
