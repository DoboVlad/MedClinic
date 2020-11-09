using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Helpers;
using mobile.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        ObservableCollection<Appointment> aplist = new ObservableCollection<Appointment>();
        private Appointment oldAppointment;
        public HomePage()
        {
            InitializeComponent();

            // dummy data to see the functionality of the list view, should be put in a HomePageModel
            
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active", Details = "You were diagnosed with diarrhea... Sorry, mate!" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active", Details = "You were diagnosed with diarrhea... Sorry, mate!" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });

            appointmentsList.ItemsSource = aplist;
            // bind the picker to enable translation
            pickerSort.ItemsSource = new List<string> { AppResources.Active, AppResources.Inactive, AppResources.All };

            appointmentsList.ItemTapped += OnItemTapped;


        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var appointment = e.Item as Appointment; // conversion
            HideOrShowAppointment(appointment);

        }
      
        private void HideOrShowAppointment(Appointment a) {

            if (oldAppointment == a)
            {
                //click twice to hide the details
                a.IsVisible = !a.IsVisible;
                UpdateAppointments(a);

            }
            else {
                if (oldAppointment != null)
                {
                    // hide the old selected item details
                    oldAppointment.IsVisible = false;
                    UpdateAppointments(oldAppointment);
                }
                // show selected item details
                a.IsVisible = true;
                UpdateAppointments(a);

            }
            oldAppointment = a;

        }
        // this is used to trigger the obs collection changed items in order to show the user the changed ViewCell
        private void UpdateAppointments(Appointment a)
        {
            var index = aplist.IndexOf(a);
            aplist.Remove(a);
            aplist.Insert(index, a);
        }
    }
}