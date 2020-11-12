using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Resources;
using mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
            
            
        }

       async private void btnLogIn_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Xamarin.Forms.Button)sender);

            // chose modal in order to prevent the navigation bar from the home screen. After logging in, the user shouldn't be able to get back to that screen
            // only if he logs out
            if (entUsername.Text == "0")
                await Navigation.PushAsync(new TabbedMainPageDoctor());
            else await this.Navigation.PushAsync(new TabbedPageMain());
        }
       
    }
}