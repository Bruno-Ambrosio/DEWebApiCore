using DEWebApi.Models;

namespace DEWebApi.Dto.Patient
{
    public class CreatePatientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public int GenderId { get; set; }
    }
}
