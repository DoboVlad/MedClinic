using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        private AboutUsModel aum = new AboutUsModel();
        public AboutUsPage()
        {
            InitializeComponent();
            
           

            doctorsList.ItemsSource = aum.doctorList;
            // added an event on item tapped 
            doctorsList.ItemTapped += OnItemTapped;
        }
        // any item tapped will open another page with more details about the doctor
         public void OnItemTapped(object sender, ItemTappedEventArgs e) => Navigation.PushAsync(new AboutUsPageDetails((DoctorModel)e.Item));
    }
}