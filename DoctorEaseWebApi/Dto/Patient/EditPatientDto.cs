namespace DEWebApi.Dto.Patient
{
    public class EditPatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public int GenderId { get; set; }
    }
}
