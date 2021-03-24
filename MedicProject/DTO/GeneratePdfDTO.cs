namespace MedicProject.DTO
{
    public class GeneratePdfDTO
    {
        public int id { get; set; }

        public string sendTo { get; set; }

        public string reason { get; set; }

        public string diagnostic { get; set; }      

        public string treatment { get; set; }
    }
}