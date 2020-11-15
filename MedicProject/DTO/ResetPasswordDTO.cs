using System;
using System.ComponentModel.DataAnnotations;

namespace MedicProject.DTO
{
    public class ResetPasswordDTO
    {
        
        [Required(ErrorMessage = "New password required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$",ErrorMessage="The password must be at least 8 characters long and it must contain at least one uppercase letter, one lowercase letter and one number")]
        public string newPassword { get; set; }

        [Compare(nameof(newPassword), ErrorMessage = "The passwords don't match.")]
        public string repeatPassword {get;set;}

        [Required]
        public string resetCode { get; set; }
    }
}