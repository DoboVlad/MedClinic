using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    class RegisterBindingModel
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string cnp { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string phoneNumber { get; set; }
        public string password { get; set; }

        public String repeatPassword { get; set; }

        public int doctorId { get; set; }
    }
}
