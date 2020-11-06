using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Resources;
using Xamarin.Forms;

namespace mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
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


        }
    }
}
