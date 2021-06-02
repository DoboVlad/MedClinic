namespace MedicProject.Models
{
    public class Hour
    {
        public int Id { get; set; }
        public string startHour { get; set; }
        public string endHour {get; set;}
        public int ScheduleId { get; set; } 
        public Schedule schedule { get; set; }
    }
}