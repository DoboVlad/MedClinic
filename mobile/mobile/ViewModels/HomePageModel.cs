using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using mobile.ViewModels;

namespace mobile.ViewModels
{
    // viewModel used to do all the work needed for the HomePage
    class HomePageModel
    {
        // used for the tapped event, to close the most recent opened item in the list
        private AppointmentModel oldAppointment;
        public ObservableCollection<AppointmentModel> aplist = new ObservableCollection<AppointmentModel>();
        public HomePageModel()
        {

            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", Details = "You were diagnosed with diarrhea... Sorry, mate!", PatientName="Ionut Iga"});
            aplist.Add(new AppointmentModel { Date = "12.10.2018", Hour = "09:00", Status = "Inactive", Details = "You were diagnosed with diarrhea... Sorry, mate!", PatientName="Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName ="Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active" , PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });
            aplist.Add(new AppointmentModel { Date = "10.10.2020", Hour = "10:00", Status = "Active", PatientName = "Ionut Iga" });

        }
        public void HideOrShowAppointment(AppointmentModel a)
        {
            
            if (oldAppointment == a)
            {
                //click twice to hide the details
                a.IsVisible = !a.IsVisible;
                UpdateAppointments(a);

            }
            else
            {
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
        private void UpdateAppointments(AppointmentModel a)
        {
            var index = aplist.IndexOf(a);
            aplist.Remove(a);
            aplist.Insert(index, a);
        }

        // this method is used to enable deleting of an appointment through the button shown on the UI, from doctor
        // did like this because I cannot access directly elements of the DataTemplate
        public void OnDeleteButton(AppointmentModel app)
        {

            aplist.Remove(app);
            // to make sure the Appointment deleted is no longer referenced
            if (app == oldAppointment) oldAppointment = null;


        }
    
    }

}
