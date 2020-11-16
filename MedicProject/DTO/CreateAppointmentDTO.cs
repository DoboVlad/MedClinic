using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicProject.DTO
{
    public class CreateAppointmentDTO
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        public string hour { get; set; }

        public int UserId { get; set; }
    }
}
