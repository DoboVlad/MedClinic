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
        public List<AppointmentModel> aplist = new List<AppointmentModel>();
        public HomePageModel()
        {
            getAppts();
        }
      
        public async void getAppts() {
          aplist = await App.apiServicesManager.GetApptsAsync(App.user.token);
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
