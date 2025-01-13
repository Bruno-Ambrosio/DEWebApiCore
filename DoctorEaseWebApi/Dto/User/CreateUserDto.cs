using DoctorEaseWebApi.Enum;
using System.ComponentModel.DataAnnotations;

namespace DoctorEaseWebApi.Dto.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Name must be informed!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email must be informed!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must be informed!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password must be informed!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Role must be informed!")]
        public RoleEnum Role { get; set; }
    }
}
