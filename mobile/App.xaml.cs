using System;
using mobile.Models;
using mobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    public partial class App : Application
    {
        public static ApiServicesManager apiServicesManager { get; private set; }
        public static User user { get; set; }
        public App()
        {
            InitializeComponent();

            apiServicesManager = new ApiServicesManager(new ApiServices());
            user = new User();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
