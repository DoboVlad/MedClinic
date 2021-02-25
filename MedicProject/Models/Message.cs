using MedicProject.Models;
using System;

namespace MedicProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        
        public int TransmitterId { get; set; }

        public string TransmitterEmail { get; set; }
        public User Transmitter { get; set; }

        public int ReceiverId { get; set; }

        public string ReceiverEmail { get; set; }
        public User Receiver { get; set; }

        public string Content { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime DateSent { get; set; } = DateTime.Now;
    }
}