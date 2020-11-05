using System;

namespace MedicProject.DTO
{
    public class NextOrHistoryAppointmentsDTO
    {
        public int Id {get; set;}
        public DateTime date { get; set; }

        public string hour  { get; set; }
        public UserDTO user{get; set;}
    }
}