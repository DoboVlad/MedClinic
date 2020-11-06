using Microsoft.Win32.SafeHandles;
using mobile.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        async private void btnRegister_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Button)sender);

            // to verify if the date of birth was chosen
            int result = DateTime.Compare(dpBirthDate.Date, DateTime.Today);

            //verify if fiels are filled in
            if (result==0 ||  entFirstName == null || entLastName == null || entEmail == null || entPhone == null || entCnp == null ||  entPlaceOfBirth == null || entAdress == null || pckDoctor== null)
            {
                await DisplayAlert("You didn't complete all the fields.", "Would you like to try again?", "Yes", "No");
            }
            else if(entPassword != entConfPwd )
            {
                await DisplayAlert("Your password don't match.", "Would you like to try again?", "Yes", "No");
            }


            //email validation
            var email = entEmail.Text;
            Regex emailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (!emailPattern.IsMatch(email)) { 
               await DisplayAlert ("Your email is invalid.", "Would you like to try again?", "Yes", "No");
            }
            else
            {
                await this.Navigation.PushAsync(new RegistrationSucceedPage());
            }
            
        }
    }
}