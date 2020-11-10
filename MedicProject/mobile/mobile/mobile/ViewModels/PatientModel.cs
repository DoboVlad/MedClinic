using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    class PatientModel : Patient
    {
        

        public string FullName
        {
            get
            {

                return FirstName + " " + LastName;
            }

        }
    }
}
