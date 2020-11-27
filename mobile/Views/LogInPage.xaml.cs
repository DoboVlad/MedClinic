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
       
    {   
       
        Login login = new Login();
        public LogInPage()

        { Console.WriteLine("Creat");
            InitializeComponent();
            BindingContext = login;
        }

 

        async private void ForgettenPwd_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ForgotPwdPage());
        }
    }
}