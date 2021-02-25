using System;
using MedicProject.Models;

namespace MedicProject.DTO
{
    public class MessageDto
    {
        public int Id { get; set; }
        
        public int TransmitterId { get; set; }

        public string TransmitterEmail { get; set; }

        public string TransmitterFirstName { get; set; }
        
        public int ReceiverId { get; set; }

        public string ReceiverEmail { get; set; }
        public string ReceiverFirstName { get; set; }

        public string Content { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime DateSent { get; set; }
    }
}