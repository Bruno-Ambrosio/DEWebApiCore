using DoctorEaseWebApi.Models;

namespace DoctorEaseWebApi.Dto.Auth
{
    public class AuthDto
    {
        public UserModel User { get; set; } = new UserModel();
        public string Token { get; set; } = string.Empty;
    }
}
