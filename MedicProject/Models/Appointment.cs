using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicProject.Models
{
    [Table("APPOINTMENTS")]
    public class Appointment
    {
        public int Id { get; set; }
      
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        public string name { get; set; }

        public string hour { get; set; }

        public string endHour { get; set; }

        public User User {get; set;}

        public int? UserId {get;set;}
    }
}