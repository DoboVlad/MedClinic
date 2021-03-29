using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicProject.Models
{
    public class Result
    {
        public int Id { get; set; }

        public string result { get; set; }

        public ICollection<Medicine> MedicineId { get; set; }
    }
}
