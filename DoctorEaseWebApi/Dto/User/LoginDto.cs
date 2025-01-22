using System.ComponentModel.DataAnnotations;

namespace DoctorEaseWebApi.Dto.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email must be informed!")]
        public string Email { get; set;} = string.Empty;
        [Required(ErrorMessage = "Password must be informed!")]
        public string Password { get; set; } = string.Empty;
    }
}
