using DoctorEaseWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DoctorEaseWebApi.Services.Password
{
    public class PasswordService : IPasswordInterface
    {
        private readonly IConfiguration _Configuration;

        public PasswordService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void CreateHashPassword(string password, out byte[] hashPassword, out byte[] saltPassword)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                saltPassword = hmac.Key;
                hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyHashPassword(string password, byte[] hashPassword, byte[] saltPassword)
        {
            using (HMACSHA512 hmac = new HMACSHA512(saltPassword))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hashPassword);
            }
        }

        public string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Name", user.Name.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("Role", user.Role.ToString()),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_Configuration.GetSection("JWT:Key").Value ?? string.Empty));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
