using System.Collections.Generic;

namespace MedicProject.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public string day { get; set; }

        public List<Hour> Hour { get; set; }

        public int UserId { get; set; }
        
        public User user { get; set; }
    }
}