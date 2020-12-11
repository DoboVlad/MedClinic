using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace mobile
{
    public class Doctor
    {// this is a dummy class used to test the way about us page looks like
       
        private string firstName;
        private string lastName;
        private string email;
        private const string jobTitle = "Family Doctor";
        private string phone;
        private int age;
        private ImageSource image;
        private string description;


        public int Id { get; set; }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        } 
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public ImageSource Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
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
        public string JobTitle
        {
            get
            {
                return jobTitle;
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
