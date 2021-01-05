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
        
       
        public HomePageDoctor()

        { 
       
            InitializeComponent();
 
            App.hpmd = new HomePageModelDoctor();

            BindingContext = App.hpmd;
            // bind the picker to enable translation
            pickerSort.ItemsSource = new List<string> { AppResources.Active, AppResources.Inactive };

            appointmentsList.ItemTapped += OnItemTapped;
            pickerSort.SelectedIndexChanged += PickerSort_SelectedIndexChanged;



        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
 
            App.hpmd.getNextAppts();

        }
        public void PickerSort_SelectedIndexChanged(object sender, EventArgs e)
        {

            int value = pickerSort.SelectedIndex;
            switch (value)
            {
                case 0:
                    {

                        App.hpmd.getNextAppts();
                    }
                    break;
                case 1:
                    {
                        App.hpmd.getOldAppts();

                    }
                    break;
                   

            }
        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var appointment = e.Item as AppointmentModel; // conversion
            if (appointment.IsActive.Equals("Status: " + AppResources.Active))
            App.hpmd.HideOrShowAppointment(appointment);

        }

        // did like this because I cannot access directly elements of the DataTemplate
        // I am using my hpm and the last Item Tapped to enable its deletion



    }
}