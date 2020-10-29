using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicProject.Models
{
    [Table("Appointments")]
    public class Appointments
    {
        public int Id { get; set; }
      
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        public string hour { get; set; }

        public User User {get; set;}

        public int UserId {get;set;}
        
    }
}