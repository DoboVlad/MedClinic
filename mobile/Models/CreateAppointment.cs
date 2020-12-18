using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.Models
{
    public class CreateAppointment
    {
        private DateTime date;
        private string hour;
        private int userId;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }

        }
        public string Hour
        {
            get
            {
                return hour;
            }
            set
            {
                hour = value;
            }
        }
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }
    }
}
