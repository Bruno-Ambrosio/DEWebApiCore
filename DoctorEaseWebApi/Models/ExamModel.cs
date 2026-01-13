namespace DEWebApi.Models
{
    public class ExamModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime Date { get; set;  }
        public int PatientId { get; set; }
        public PatientModel? Patient { get; set; }
    }
}
