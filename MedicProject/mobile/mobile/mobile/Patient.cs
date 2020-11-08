using System;
using System.Collections.Generic;
using System.Text;

namespace mobile
{
    // dummy class used to display the user profile.
    class Patient
    {
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private string birthDate;
        // Personal Identification Number
        private string pin;
        private string birthPlace;
        private string phone;


        public string Address
        {
            get
            {
                return  address;
            }
            set
            {
                address = value;
            }
        }
      
        public string PIN
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
        public string BirthPlace
        {
            get
            {
                return  birthPlace;
            }
            set
            {
                birthPlace = value;
            }
        }
        public string FullName
        {
            get
            {

                return firstName + " " + lastName;
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
