namespace DEWebApi.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public int GenderId { get; set; }
        public GenderModel Gender { get; set; } = new GenderModel();
        public bool Active { get; set; } = true;
        public string AdditionalInfo { get; set; } = string.Empty;
    }
}
