namespace DoctorEaseWebApi.Models
{
    public class ResponseModel<T>
    {
        public T? Content { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
    }
}
