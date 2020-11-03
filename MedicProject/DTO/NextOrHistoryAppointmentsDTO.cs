using System;

namespace MedicProject.DTO
{
    public class NextOrHistoryAppointmentsDTO
    {
        public NextOrHistoryAppointmentsDTO(int id, string firstName, string lastName, DateTime date, string hour, string phone)
        {
            Id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.date = date;
            this.hour = hour;
            this.phone = phone;
        }

        public int Id {get; set;}
        public string firstName { get; set; }
        public string lastName { get; set; }

        public DateTime date { get; set; }

        public string hour  { get; set; }
        public string phone { get; set; }
    }
}