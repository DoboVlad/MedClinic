using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidatePage : ContentPage
    {
        public ValidatePage()
        {
            InitializeComponent();
 

        }



        async private void btnValidate_Clicked(object sender, EventArgs e)
        {
            bool isValid = await App.apiServicesManager.ValidateCode(entValidateCode.Text);
            if (isValid)
                await Application.Current.MainPage.Navigation.PushAsync(new TabbedPageMain());
            else entValidateCode.BackgroundColor = Color.Red;
        }

        private void entValidateCode_Focused(object sender, FocusEventArgs e)
        {
            entValidateCode.BackgroundColor = Color.Default;

        }
    }
}