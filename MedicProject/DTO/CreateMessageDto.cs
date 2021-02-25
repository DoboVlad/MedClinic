using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicProject.DTO
{
    public class CreateMessageDto
    {
        public string ReceiverEmail { get; set; }

        public string Content { get; set; }

    }
}
