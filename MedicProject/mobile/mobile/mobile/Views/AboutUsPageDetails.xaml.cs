using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Resources;
using mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPageDetails : ContentPage
    {
        
        public AboutUsPageDetails(DoctorModel doctor)
        {
            InitializeComponent();
            BindingContext = doctor;
            // to show the name of the doctor
            Title = AppResources.About + " " + doctor.FullName;
        }
    }
}