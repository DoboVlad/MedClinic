using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileModel profileModel = new ProfileModel();
        public ProfilePage()
        {
            InitializeComponent();
            var patient = profileModel.patient;

        }
        async private void btnLogOut_Clicked(Object sender, EventArgs e)
        {
           
            App.user.token = "";

            await this.Navigation.PushAsync(new MainPage());
        }

         private void  OnSettingsTapped(object sender, EventArgs e)
        {
            Page page = new SettingsPage();
            Navigation.PushAsync(page);
            NavigationPage.SetHasNavigationBar(page, false);
            //await Navigation.PushAsync(new SettingsPage());
        }
    }
}