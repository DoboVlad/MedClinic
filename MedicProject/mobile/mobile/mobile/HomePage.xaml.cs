using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            // dummy data to see the functionality of the list view
            List<Appointment> aplist = new List<Appointment>();
            aplist.Add(new Appointment {Date="10.10.2020",Hour="10:00",Status="Active" });
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
            aplist.Add(new Appointment { Date = "10.10.2020", Hour = "10:00", Status = "Active" });

            

            list.ItemsSource = aplist;


        }
    }
}