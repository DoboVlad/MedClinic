using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;
using mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePageDoctor : ContentPage
    {
        public ProfilePageDoctor()
        {
            InitializeComponent();
            DoctorModel Cosmina = new DoctorModel()
            {
                FirstName = "Cosmina",
                LastName = "Anegrului",
                Phone = "1234567890",

                Email = "cosmina.anegrului.yahoo.com"
            };

            BindingContext = Cosmina;
            // here is going to come the real doctor image, not a placeholder
        

        }
        async private void btnLogOut_Clicked(Object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new MainPage());
        }

        async private void OnSettingsTapped(object sender, EventArgs e)
        {
            Page page = new SettingsPageDoctor();
            await Navigation.PushAsync(page);
            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}