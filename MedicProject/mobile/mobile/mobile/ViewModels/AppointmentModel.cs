using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    class AppointmentModel : Appointment
    {
        // testing the visibility of the details
        public bool IsVisible { get; set; }

        public string AppointmentDate
        {

            get
            {

                return "APPOINTMENT AT " + Hour + ", ON " + Date;
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
    }
}
