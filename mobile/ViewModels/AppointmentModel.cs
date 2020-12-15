using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class AppointmentModel : Appointment
    {
        // testing the visibility of the details
        public bool IsVisible { get; set; }

        public string AppointmentDate
        {

            get
            {

                return "APPOINTMENT AT " + Hour + ", ON " + DateString;
            }

        }
        // if the appointment is active or not
        public string IsActive
        {
            get
            {
                return "Status: " + Status;
            }

        }
        // to get the name displayed as wanted, with the format Patient: FullName
        public string PatientNameFormat
        {
            get
            {
                return "Patient: " + PatientName;

            }
        }
        public ICommand DeleteApptCommand

        {
            get
            {
                return new Command(() =>
                {
                    App.hpm.DeleteAppt();
                });
            }
        }
    }
}
