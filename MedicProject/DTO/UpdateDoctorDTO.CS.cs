using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MedicProject.DTO
{
    public class UpdateDoctorDTO
    {
        [Required(ErrorMessage="Please enter your first name.")]
        [RegularExpression("^[A-Za-z ,.'-]+$",ErrorMessage="The first name can only contain letter")]
        public string firstName { get; set; }
 
        [Required(ErrorMessage="Please enter your last name.")]
        [RegularExpression("^[A-Za-z ,.'-]+$",ErrorMessage="The last name can only contain letter")]
        public string lastName { get; set; }
       
        [Required(ErrorMessage="Please enter your email.")]
        [EmailAddress(ErrorMessage="Incorrect email format")]
        public string email { get; set; }
     
        [Required(ErrorMessage="Please enter your phone number.")]
        [Phone(ErrorMessage="Invalid phone number format")]
        public string phoneNumber{get;set;}
      
        [Required]
        public string description { get; set; }
    
        public IFormFile photo {get;set;}
    }
}