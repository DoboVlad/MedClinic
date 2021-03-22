using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicProject.Models
{
    [Table("APPOINTMENTS")]
    public class Appointments
    {
        public int Id { get; set; }
      
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        public string name { get; set; }

        public string hour { get; set; }

        public string endHour { get; set; }

        public string Result { get; set; }

        public User User {get; set;}

        public int? UserId {get;set;}
    }
}