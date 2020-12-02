using System.ComponentModel.DataAnnotations;

namespace MedicProject.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }

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

        public int isApproved { get; set; }

        public int age { get; set; }
    
       public string photo {get;set;}

    }
}