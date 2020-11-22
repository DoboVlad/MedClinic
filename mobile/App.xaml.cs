using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    public partial class App : Application
    {
        public string Ion;
        public App()
        {
            InitializeComponent();

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
