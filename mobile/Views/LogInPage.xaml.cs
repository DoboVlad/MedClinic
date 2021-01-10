using System;
using System.Windows.Input;
using mobile.Views;
using Xamarin.Forms;
using mobile.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
       
    {   
       
        public LogInPage()

        { Console.WriteLine("Creat");
            InitializeComponent();

        }

 

        async private void ForgettenPwd_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ForgotPwdPage());
        }

        private async void btnLogInn_Clicked(object sender, EventArgs e)
        {
           bool IsLogged = await App.apiServicesManager.LoginAsync(entUsername.Text, entPassword.Text);

            if (IsLogged == true)
            {
                if (App.user.role == 1)
                {

                    await Navigation.PushAsync(new TabbedMainPageDoctor());
                }
                else if (App.user.Validated == 1)
                    await Navigation.PushAsync(new TabbedPageMain());
                else await Navigation.PushAsync(new ValidatePage());
            }
            else
            {
                entUsername.BackgroundColor = Color.Red;
                entPassword.BackgroundColor = Color.Red;
            }
        }

        private void entUsername_Focused(object sender, FocusEventArgs e)
        {

            entUsername.BackgroundColor = Color.Default;
        }

        private void entPassword_Focused(object sender, FocusEventArgs e)
        {

            entPassword.BackgroundColor = Color.Default;
        }
    }
}