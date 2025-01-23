using DEWebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DoctorEaseWebApi.Dto.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Name must be informed!")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email must be informed!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password must be informed!")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password must be informed!")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role must be informed!")]
        public int RoleId { get; set; }
    }
}
