using System.ComponentModel.DataAnnotations;

namespace MedicProject.DTO
{
    public class ChangePasswordDTO
    {   

        [Required]
        public string oldPassword { get; set; }
       
        [Required]
        public string newPassword { get; set; }

        [Compare(nameof(newPassword), ErrorMessage = "The passwords don't match.")]
        public string repeatPassword {get;set;}

    }
}