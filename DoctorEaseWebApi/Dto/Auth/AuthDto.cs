using DoctorEaseWebApi.Models;

namespace DoctorEaseWebApi.Dto.Auth
{
    public class AuthDto
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}
