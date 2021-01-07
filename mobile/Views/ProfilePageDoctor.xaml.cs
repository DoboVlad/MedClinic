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
        public ProfileModel profileModelDoc = new ProfileModel();
        public ProfilePageDoctor()
        {
            InitializeComponent();
            // here is going to come the real doctor image, not a placeholder
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = profileModelDoc.doctor;
        }

        async private void btnLogOut_Clicked(Object sender, EventArgs e)
        {
            App.user.token = "";
            await this.Navigation.PushAsync(new MainPage());
        }

        async private void OnSettingsTapped(object sender, EventArgs e)
        {
            Page page = new SettingsPageDoctor(profileModelDoc.doctor);
            await Navigation.PushAsync(page);
            NavigationPage.SetHasNavigationBar(page, false);
        }
        async private void Guide_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new HelpPage());
        }
    }
}