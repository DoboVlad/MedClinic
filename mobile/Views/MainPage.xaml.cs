using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using mobile.Resources;
using mobile.ViewModels;
using Xamarin.Forms;

namespace mobile
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public MainPage()
        { 
            NavigationPage.SetHasBackButton(this, false);
     
            InitializeComponent(); 


        }


        async private void btnLogIn_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Button)sender);
            await this.Navigation.PushAsync(new LogInPage());
        }
      
        async private void btnRegister_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Button)sender);
            await this.Navigation.PushAsync(new RegistrationPage());
        }
        async private void btnAboutUs_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Button)sender);

            await this.Navigation.PushAsync(new AboutUsPage());


        }
      // prevend user from going back after logged out
    }
}
