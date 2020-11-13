using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    class PatientModel : Patient
    {

        // testing the visibility of the details
        public bool IsVisible { get; set; }

        public string FullName
        {
            get
            {

                return FirstName + " " + LastName;
            }

        }
    }
}
