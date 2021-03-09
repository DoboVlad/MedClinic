namespace MedicProject.Models
{
    public class Hour
    {
        public int Id { get; set; }

        public string hour { get; set; }

        public int Availability { get; set; } = 1;    

        public int ScheduleId { get; set; } 

        public Schedule schedule { get; set; }
    }
}