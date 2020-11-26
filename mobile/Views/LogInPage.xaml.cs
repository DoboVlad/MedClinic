using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Resources;
using mobile.Views;
using Xamarin.Forms;
using mobile.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    { Login login = new Login();
        public LogInPage()
        {
            InitializeComponent();
            BindingContext = login;
        }

       async private void btnLogIn_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Xamarin.Forms.Button)sender);

            // chose modal in order to prevent the navigation bar from the home screen. After logging in, the user shouldn't be able to get back to that screen
            // only if he logs out
            login.LoginCommand.Execute(null);

            if (login.IsLogged == true)
            {
                if (App.user.role == 1)
                    await Navigation.PushAsync(new TabbedMainPageDoctor());
                else await this.Navigation.PushAsync(new TabbedPageMain());
            }
       }

        async private void ForgettenPwd_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ForgotPwdPage());
        }
    }
}