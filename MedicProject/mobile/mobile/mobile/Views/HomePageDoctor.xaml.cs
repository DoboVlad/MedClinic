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
    public partial class HomePageDoctor : ContentPage
    {
        // for enabling deletion, saving the last itemTapped 
        private AppointmentModel appTapped;
        private HomePageModel hpm = new HomePageModel();
        public HomePageDoctor()
        {
            InitializeComponent();

           
            
            appointmentsList.ItemsSource = hpm.aplist;
            // bind the picker to enable translation
            pickerSort.ItemsSource = new List<string> { AppResources.Active, AppResources.Inactive, AppResources.All };

            appointmentsList.ItemTapped += OnItemTapped;
            
            

        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var appointment = e.Item as AppointmentModel; // conversion
            hpm.HideOrShowAppointment(appointment);
            appTapped = appointment;

        }

        // did like this because I cannot access directly elements of the DataTemplate
        // I am using my hpm and the last Item Tapped to enable its deletion
        private void DeleteButton_Clicked(Object sender, EventArgs e)
        {

            hpm.OnDeleteButton(appTapped);
        }

      
    }
}