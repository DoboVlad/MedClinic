using System.ComponentModel.DataAnnotations;

namespace MedicProject.DTO
{
    public class LoginDTO
    {
        [Required]
        public string email{get;set;}
        
        [Required]
        public string password{get;set;}
    }
}