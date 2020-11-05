using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MedicProject.Extensions;

namespace MedicProject.Models
{
    public class User
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
        public string email { get; set; }

        public string cnp {get;set;}
        
        [DataType(DataType.Date)]
        public DateTime dateOfBirth{get; set;}

        public string phoneNumber{get;set;}

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt {get;set;}
        public int isApproved { get; set; }=0;
        public int isMedic { get; set;}=0;

        public string description { get; set; }
        public string photo {get;set;}
        public User doctor{get;set;}
        public int? doctorId { get; set; }

        public ICollection<User> Pacients{get;set;}
        public ICollection<Appointments> Appointments {get; set;}

        public int Getage(){
          return dateOfBirth.CalculateAge();
        }
        

    }
}
