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
        public ProfilePage()
        {
            InitializeComponent();
            PatientModel Ionut = new PatientModel() 
            {FirstName = "Ionut",
                LastName="Iga",
                Phone="1234567890",
                PIN="1992753857251" ,
                BirthDate="25.09.1999",
                Email="ionut.iga@yahoo.com" };

            BindingContext = Ionut;
            if (int.Parse(Ionut.PIN.Substring(0, 1)) % 2 != 0)
                imgPatient.Source = ImageSource.FromResource("mobile.Images.manWhite.png");
            else
                imgPatient.Source = ImageSource.FromResource("mobile.Images.womanWhite.png");

        }
        async private void btnLogOut_Clicked(Object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new MainPage());
        }

        async private void  OnSettingsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}