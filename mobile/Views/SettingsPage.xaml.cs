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

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        Regex emailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

        PatientModel patientModel = new PatientModel();
        public SettingsPage(PatientModel patient)
        {
            InitializeComponent();
            patientModel = patient;
            BindingContext = patientModel;
        }

        async private void TapGestureRecognizer_Cancel(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }
        async private void TapGestureRecognizer_Done(object sender, EventArgs e)
        {
            if (entLast.Text != "" && entPhone.Text != "" && entEmail.Text != "")
            {
                if (!emailPattern.IsMatch(entEmail.Text))
                {
                    bool response = await DisplayAlert(AppResources.AlertEmail, AppResources.AlertChange, AppResources.Yes, AppResources.No);
                    if (!response)
                    {
                        await this.Navigation.PopAsync();
                    }
                }
                else
                {
                    bool result = await DisplayAlert(AppResources.Hei, AppResources.SaveChanges, AppResources.Yes, AppResources.No);
                    if (result)
                    {
                        patientModel.UpdateCommand.Execute(null);
                        await this.Navigation.PopAsync();
                    }
                }

            }
            else {
               bool response=  await DisplayAlert("Salut, ai introdus campuri cu valoare nula, acestea nu vor fi salvate", "Doriti sa iesiti?", "Yes", "Nu");
                if (response)
                {
                    await this.Navigation.PopAsync();
                }
            }

        }

    }
}