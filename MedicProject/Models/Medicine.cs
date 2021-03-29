using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicProject.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        public string name { get; set; }

        // quantity on 24h
        public int dosage { get; set; }
    }
}
