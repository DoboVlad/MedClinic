using System;
using mobile.Models;
using mobile.Services;
using mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    public partial class App : Application
    {
        public static  ApiServicesManager apiServicesManager { get; private set; }
        public static User user { get; set; }
        public static AboutUsModel aum;
        public static PatientListModel plm;
        public static HomePageModel hpm;
     
        public App()
        {
            InitializeComponent();

            apiServicesManager = new ApiServicesManager(new ApiServices());
            user = new User();
            aum = new AboutUsModel();
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
