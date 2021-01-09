using mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobile
{
    // dummy class used to display the user profile.
     public class Patient
    {
        private string firstName;
        private string lastName;
        private string email;

        private string birthDate;
        // Personal Identification Number
        private string pin;
        private string phone;
        private DoctorModel doctor;


  
        public DoctorModel Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
            }
        }

        public int age { get; set; }

        public int Id { get; set; }
        public string cnp
        {
            get
            {
                return pin;
            }
            set
            {
                pin = value;
            }
        }
        public string BirthDate
        {
            get
            {
                return  birthDate;
            }
            set
            {
                birthDate = value;
            }
        }
       
        

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
       
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }
    }
}
