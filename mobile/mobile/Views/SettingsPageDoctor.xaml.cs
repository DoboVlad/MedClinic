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
    public partial class SettingsPageDoctor : ContentPage
    {
        public SettingsPageDoctor()
        {
            InitializeComponent();
        }
        async private void TapGestureRecognizer_Cancel(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }
        async private void TapGestureRecognizer_Done(object sender, EventArgs e)
        {
            // name verifications?

            Regex emailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

            /*
                 if (!emailPattern.IsMatch(entEmail.Text))
                 {
                     await DisplayAlert(AppResources.AlertEmail, AppResources.AlertChange, AppResources.Yes, AppResources.No);
                 }
                 else
                 {
                     await DisplayAlert("Hei,", "Are you sure you want to save the changes?", AppResources.No, AppResources.Yes);
                 }*/
            bool result = await DisplayAlert(AppResources.Hei, AppResources.SaveChanges, AppResources.Yes, AppResources.No);
            if (result)
            {
                await this.Navigation.PopAsync();
            }

        }
    }
}