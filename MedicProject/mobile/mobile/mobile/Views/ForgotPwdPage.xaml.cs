using mobile.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPwdPage : ContentPage
    {
        public ForgotPwdPage()
        {
            InitializeComponent();
        }
        Regex emailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

        async private void btnSentEmail_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entEmail.Text))
            {
                bool result = await DisplayAlert(AppResources.EmptyEmail, AppResources.Exit, AppResources.Yes, AppResources.No);
                if (result)
                {
                    await Navigation.PopAsync();
                }
            }else if (!emailPattern.IsMatch(entEmail.Text))
            {
                bool result = await DisplayAlert(AppResources.AlertEmail, AppResources.AlertChange, AppResources.Yes, AppResources.No);
                if (result == false)
                {
                    await this.Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                lblEmailMess.IsVisible = true;
                entEmail.IsEnabled = false;
                btnVerifyCode.IsVisible = true;
                btnSentEmail.IsVisible = false;
                entCode.IsVisible = true;
                entTryAgain.IsVisible = true;
                //sent email
            }
        }

        private void btnVerifyCode_Clicked(object sender, EventArgs e)
        {
            // verify code

        }

        private void TryAgain_Tapped(object sender, EventArgs e)
        {
            lblEmailMess.IsVisible = false;
            entEmail.IsEnabled = true;
            btnVerifyCode.IsVisible = false;
            btnSentEmail.IsVisible = true;
            entCode.IsVisible = false;
            entTryAgain.IsVisible = false;
        }
    }
}