using mobile.Resources;
using mobile.ViewModels;
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
        bool x = true;
        public ForgotPwdPage()
        {
            InitializeComponent();
        }
        Regex emailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
        //send email 
        async private void btnSentEmail_Clicked(object sender, EventArgs e)
        {
            //verify if email is valid
            if (string.IsNullOrWhiteSpace(entEmail.Text))
            {
                bool result = await DisplayAlert(AppResources.EmptyEmail, AppResources.Exit, AppResources.Yes, AppResources.No);
                if (!result)
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
                //if is valid send email
                if(await App.apiServicesManager.SendEmail(entEmail.Text))
                {
                    //if no error occurred
                    lblEmailMess.IsVisible = true;
                    entEmail.IsEnabled = false;
                    btnVerifyCode.IsVisible = true;
                    btnSentEmail.IsVisible = false;
                    entCode.IsVisible = true;
                    entTryAgain.IsVisible = true;
                    entEmail.IsVisible = true;
                    entPwd.IsVisible = true;
                    entPwdConf.IsVisible = true;
                }
                else
                {
                    //message for server error
                    lblEmailMess.IsVisible = true;
                    lblEmailMess.Text = AppResources.ErrorTryAgain;
                }

                
            }
        }
        //verify code and set password
        async private void btnVerifyCode_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(entPwdConf.Text) || string.IsNullOrWhiteSpace(entPwd.Text) || string.IsNullOrWhiteSpace(entCode.Text))
            {
                bool res = await DisplayAlert(AppResources.AlertFillField, AppResources.AlertChange, AppResources.Yes, AppResources.No);
                if (res == false)
                {
                    await this.Navigation.PushAsync(new MainPage());
                }
            }else if (entPwd.Text.Length < 6)
            {
                bool res = await DisplayAlert(AppResources.AlertPwdShort, AppResources.AlertChange, AppResources.Yes, AppResources.No);
                if (res == false)
                {
                    await this.Navigation.PushAsync(new MainPage());
                }
            }
            else if(entPwd.Text != entPwdConf.Text)
            {
                bool res = await DisplayAlert(AppResources.AlertPwdMatch, AppResources.AlertChange, AppResources.Yes, AppResources.No);
                if (res == false)
                {
                    await this.Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                //create to object to sent it with api
                ResetPasswordModel resetPwd = new ResetPasswordModel()
                {
                    repeatPassword = entPwdConf.Text,
                    newPassword = entPwd.Text,
                    resetCode = entCode.Text,
                };
                if (await App.apiServicesManager.ResetPassword(resetPwd))
                {
                    await DisplayAlert(AppResources.Hei, AppResources.SuccessfulReset, AppResources.OK);

                }
                else
                {
                    await DisplayAlert(AppResources.Hei, AppResources.Error, AppResources.OK);
                }                
                await this.Navigation.PushAsync(new LogInPage());
            }
            
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