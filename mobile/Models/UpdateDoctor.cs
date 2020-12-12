using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mobile.Models
{
    class UpdateDoctor
    {
        private string FirstName;
        private string LastName;
        private string Email;
        private string PhoneNumber;
        private string Description;
        private ImageSource Photo;
        public string firstName
        {
            get
            {
                return FirstName;
            }
            set
            {
                FirstName = value;
            }
        }
        public string lastName
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;
            }
        }
        public string email
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }
        public string phoneNumber
        {
            get
            {
                return PhoneNumber;
            }
            set
            {
                PhoneNumber = value;
            }
        }
        public string description
        {
            get
            {
                return Description;
            }
            set
            {
                Description = value;
            }
        }
        public ImageSource photo
        {
            get
            {
                return Photo;
            }
            set
            {
                Photo = value;
            }
        } 
    }
}
