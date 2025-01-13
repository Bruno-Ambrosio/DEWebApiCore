
using DoctorEaseWebApi.Enum;
using Microsoft.AspNetCore.Identity;

namespace DoctorEaseWebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public RoleEnum Role { get; set; }
        public DateTime TokenCreatedAt { get; set; } = DateTime.Now;
    }
}
