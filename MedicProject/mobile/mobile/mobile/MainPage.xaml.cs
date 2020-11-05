using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

       async private void btnRegister_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new LogInPage());
        }
    }
}
