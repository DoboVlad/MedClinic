namespace MedicProject.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string cnp { get; set; }

        public int age { get; set; }

        public int? doctorId { get; set; }

        public MedicDTO doctor { get; set; }
    }
}