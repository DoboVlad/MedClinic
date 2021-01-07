using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;
using mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage, INotifyPropertyChanged
    {

        public ProfileModel profileModel = new ProfileModel();

        public ProfilePage()
        {
            InitializeComponent();
           
           // Patient patient2 = profileModel.patient;
         //   Task.Delay(5000).Wait();
         //   BindingContext = patient2;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = profileModel.patient;
        }


        async private void btnLogOut_Clicked(Object sender, EventArgs e)
        {
           
            App.user.token = "";

            await this.Navigation.PushAsync(new MainPage());
        }

         private void  OnSettingsTapped(object sender, EventArgs e)
        {
            Page page = new SettingsPage(profileModel.patient);
            Navigation.PushAsync(page);
            NavigationPage.SetHasNavigationBar(page, false);
            //await Navigation.PushAsync(new SettingsPage());
        }
        async private void Guide_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new HelpPage());
        }
    }
}