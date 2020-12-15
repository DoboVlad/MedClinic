using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Helpers;
using mobile.Resources;
using mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        private HomePageModel hpm = new HomePageModel();
        public HomePage()
        {
            InitializeComponent();

            

                 BindingContext = hpm.Aplist;
            // bind the picker to enable translation
            pickerSort.ItemsSource = new List<string> { AppResources.Active, AppResources.Inactive, AppResources.All };

            appointmentsList.ItemTapped += OnItemTapped;


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            
            appointmentsList.ItemsSource = hpm.Aplist;
        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var appointment = e.Item as AppointmentModel; // conversion
           hpm.HideOrShowAppointment(appointment);
            

        }
      
       
    }
}