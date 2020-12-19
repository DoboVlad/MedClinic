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
        
         List<AppointmentModel> list1 = new List<AppointmentModel>();
     

        public HomePageDoctor()

        { 
            App.hpmd = new HomePageModelDoctor();
       
            InitializeComponent();
           


            BindingContext = App.hpmd;
            // bind the picker to enable translation
            pickerSort.ItemsSource = new List<string> { AppResources.Active, AppResources.Inactive, AppResources.All };

            appointmentsList.ItemTapped += OnItemTapped;
            pickerSort.SelectedIndexChanged += PickerSort_SelectedIndexChanged;



        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            App.hpmd.getAppts();
            list1 = App.hpmd.Aplist;
        }
        public void PickerSort_SelectedIndexChanged(object sender, EventArgs e)
        {

            int value = pickerSort.SelectedIndex;
            List<AppointmentModel> list = new List<AppointmentModel>();
            App.hpmd.Aplist = list1;
            switch (value)
            {
                case 0:
                    {
                        foreach (AppointmentModel app in list1)
                            if (app.Status.Equals(AppResources.Active))
                                list.Add(app);
                        App.hpmd.Aplist = list;

                    }
                    break;
                case 1:
                    {
                        foreach (AppointmentModel app in list1)
                            if (app.Status.Equals(AppResources.Inactive))
                                list.Add(app);
                        App.hpmd.Aplist = list;

                    }
                    break;
                case 2:
                    {

                        App.hpmd.Aplist = list1;

                    }
                    break;

            }
        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var appointment = e.Item as AppointmentModel; // conversion
            App.hpmd.HideOrShowAppointment(appointment);

        }

        // did like this because I cannot access directly elements of the DataTemplate
        // I am using my hpm and the last Item Tapped to enable its deletion



    }
}