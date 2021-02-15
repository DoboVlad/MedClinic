using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicProject.DTO
{
    public class PatientDTO
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

      [Required(ErrorMessage="Please enter your cnp.")]
      [StringLength(13,MinimumLength = 13,ErrorMessage="cnp must contain exactly 13 digits")]
      [RegularExpression("^[0-9]*$",ErrorMessage="cnp can only contain digits")]
      public string cnp {get;set;}
        
      public int age{get; set;}
      
      [Required(ErrorMessage="Please enter your phone number.")]
      [Phone(ErrorMessage="Invalid phone number format")]
      public string phoneNumber{get;set;}
      public IEnumerable<CreateAppointmentDTO> Appointments { get; set; }
    }
}
