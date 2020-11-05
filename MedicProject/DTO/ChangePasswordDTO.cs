using System.ComponentModel.DataAnnotations;

namespace MedicProject.DTO
{
    public class ChangePasswordDTO
    {   [Required]
        public int Id;

        [Required]
        public string oldPassword { get; set; }
       
        [Required]
        public string newPassword { get; set; }

    }
}