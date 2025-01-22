namespace DEWebApi.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public GenderModel Gender { get; set; } = new GenderModel();
    }
}
