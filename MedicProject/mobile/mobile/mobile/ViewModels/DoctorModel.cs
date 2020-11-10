using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    public class DoctorModel : Doctor
    {
        public string FullName
        {
            get
            {

                return "Dr. " + FirstName + " " + LastName;
            }

        }
    }
}
