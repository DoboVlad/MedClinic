using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public int role { get; set; }
        public string token { get; set; }
        public int doctorId { get; set; }
        public string FullName { get { return firstName + ' ' + lastName; } }

        public int Validated
        {
            get;

            set;
        }

    }
}
