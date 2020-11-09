using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPageDetails : ContentPage
    {
        
        public AboutUsPageDetails(Doctor doctor)
        {
            InitializeComponent();
            BindingContext = doctor;
            Title = AppResources.About + " " + doctor.FullName;
        }
    }
}