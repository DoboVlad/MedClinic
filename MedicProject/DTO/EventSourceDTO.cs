using System;

namespace MedicProject.DTO
{
    public class EventSourceDTO
    {
        public string hour {get; set;}

        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }
        public int Id { get; set; }
    }
}