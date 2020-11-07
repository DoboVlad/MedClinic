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
        //regex for email validation
        Regex emailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
        async private void btnRegister_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Button)sender);

            // to verify if the date of birth was chosen
            int result = DateTime.Compare(dpBirthDate.Date, DateTime.Today);

            //verify if fiels are filled in
            if (string.IsNullOrWhiteSpace(entFirstName.Text) || string.IsNullOrWhiteSpace(entLastName.Text) || string.IsNullOrWhiteSpace(entEmail.Text) || string.IsNullOrWhiteSpace(entPhone.Text) || string.IsNullOrWhiteSpace(entCnp.Text) || string.IsNullOrWhiteSpace(entAdress.Text) || string.IsNullOrWhiteSpace(entPassword.Text) || string.IsNullOrWhiteSpace(entConfPwd.Text))
            {
                await DisplayAlert(AppResources.AlertFillField, AppResources.AlertChange, AppResources.Yes, AppResources.No);
            }
            else if (entPassword.Text != entConfPwd.Text)
            {
                await DisplayAlert(AppResources.AlertPwdMatch, AppResources.AlertChange, AppResources.Yes, AppResources.No);
            }
            else if (!emailPattern.IsMatch(entEmail.Text))
            {
                await DisplayAlert(AppResources.AlertEmail, AppResources.AlertChange, AppResources.Yes, AppResources.No);
            }else if (!cnpCheck(entCnp.Text))
            {
                await DisplayAlert(AppResources.AlertCnp, AppResources.AlertChange, AppResources.Yes, AppResources.No);
            }else if(Equals(dpBirthDate, DateTime.Now))
            {
                await DisplayAlert(AppResources.AlertBirtday, AppResources.AlertChange, AppResources.Yes, AppResources.No);
            }
            else if (Equals(entPassword.Text, entConfPwd.Text))
            {
                if (entPassword.Text.Length < 6)
                {
                    lblPassCheck.Text = AppResources.AlertPwdShort;
                    lblPassCheck.TextColor = Color.Red;
                }
                await this.Navigation.PushAsync(new RegistrationSucceedPage());
            }

            
            
        }
        //strong password check
        private void onTexChanged(object sender, TextChangedEventArgs e)
        {
            int score = 0;
            if (entPassword.Text.Length >= 6)
                score++;
            if (Regex.Match(entPassword.Text, @"/\d+/", RegexOptions.ECMAScript).Success) //contains 0-9
                score++;
            if (Regex.Match(entPassword.Text, @"/[a-z]/", RegexOptions.ECMAScript).Success && Regex.Match(entPassword.Text, @"/[A-Z]/", RegexOptions.ECMAScript).Success) //contains a-z || A-Z
                score++;
            if (Regex.Match(entPassword.Text, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success) //contains special characters
                score++;
            
            if (score == 1)
            {
                lblPassCheck.Text = AppResources.PwdWeak;
                lblPassCheck.TextColor = Color.Red;
            }
            else if (score == 2)
            {
                lblPassCheck.Text = AppResources.PwdMedium;
                lblPassCheck.TextColor = Color.OrangeRed;
            }
            else if (score == 3)
            {
                lblPassCheck.Text = AppResources.PwdStrong;
                lblPassCheck.TextColor = Color.Orange;
            }
            else if (score == 4)
            {
                lblPassCheck.Text = AppResources.PwdVeryStrong;
                lblPassCheck.TextColor = Color.Green;
            }
        }
        //doesn't work don't know why yet
         private bool  cnpCheck( string cnp) {
            if (cnp.Length == 13)
            {
                if (!Equals(cnp.Substring(0, 1), "1") || !Equals(cnp.Substring(0, 1), "2") || !Equals(cnp.Substring(0, 1), "5") || !Equals(cnp.Substring(0, 1), "6"))
                {  
                    int luna = Convert.ToInt32(cnp.Substring(3, 2));
                    if (luna <= 12)
                    {
                        int zi = Convert.ToInt32(cnp.Substring(5, 2));
                        if ((luna % 2 == 0 && zi == 30 && luna < 8) || (luna % 2 != 0 && zi == 31 && luna < 8) || (luna % 2 == 0 && zi == 31 && luna > 7) || (luna % 2 != 0 && zi == 30 && luna > 7))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

            
         
          

        }
    }

