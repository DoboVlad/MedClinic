using mobile.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSucceedPage : ContentPage
    {
        public RegistrationSucceedPage()
        {
            InitializeComponent();

        }

        async private void btnMainPage_Clicked(object sender, EventArgs e)
        {
            Animations.Button_Scale_Clicked((Button)sender);
            await this.Navigation.PushAsync(new LogInPage());
        }
    }
}