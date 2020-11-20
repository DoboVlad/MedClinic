namespace MedicProject.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        public string email{get;set;}
        public string firstName{get;set;}
        public string lastName{get;set;}
        
        public int role{get;set;}
        public string token{get;set;}

        public int isApproved { get; set; }
        public int doctorId { get; set; }
    }
}