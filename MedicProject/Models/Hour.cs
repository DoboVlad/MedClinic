namespace MedicProject.Models
{
    public class Hour
    {
        public int Id { get; set; }

        public string startHour { get; set; }

        public string endHour {get; set;}

        // this might be useless
        public int Availability { get; set; } = 1;    

        public int ScheduleId { get; set; } 

        public Schedule schedule { get; set; }
    }
}