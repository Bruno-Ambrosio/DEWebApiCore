
using DEWebApi.Models;

namespace DoctorEaseWebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] HashPassword { get; set; } = new byte[0];
        public byte[] SaltPassword { get; set; } = new byte[0];
        public RoleModel Role { get; set; } = new RoleModel();
        public DateTime TokenCreatedAt { get; set; } = DateTime.UtcNow;
    }
}
