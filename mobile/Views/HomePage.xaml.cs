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
        List<AppointmentModel> list1 = new List<AppointmentModel>();

        public HomePage()
        {
            InitializeComponent();

            App.hpm = new HomePageModel();
            
            BindingContext = App.hpm;
            // bind the picker to enable translation
            pickerSort.ItemsSource = new List<string> { AppResources.Active, AppResources.Inactive, AppResources.All };

            appointmentsList.ItemTapped += OnItemTapped;
            pickerSort.SelectedIndexChanged += PickerSort_SelectedIndexChanged;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            App.hpm.getAppts();
            list1 = App.hpm.Aplist;
        }
        public void PickerSort_SelectedIndexChanged(object sender, EventArgs e)
        {

            int value = pickerSort.SelectedIndex;
            List<AppointmentModel> list = new List<AppointmentModel>();
            App.hpm.Aplist = list1;
            switch (value)
            {
                case 0:
                    {
                        foreach (AppointmentModel app in list1)
                            if (app.Status.Equals(AppResources.Active))
                                list.Add(app);
                        App.hpm.Aplist = list;

                    }
                    break;
                case 1:
                    {
                        foreach (AppointmentModel app in list1)
                            if (app.Status.Equals(AppResources.Inactive))
                                list.Add(app);
                        App.hpm.Aplist = list;

                    }
                    break;
                case 2:
                    {

                        App.hpm.Aplist = list1;

                    }
                    break;

            }
        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var appointment = e.Item as AppointmentModel; // conversion
           App.hpm.HideOrShowAppointment(appointment);
            

        }
      
       
    }
}