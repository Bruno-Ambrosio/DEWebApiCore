using DoctorEaseWebApi.Models;

namespace DoctorEaseWebApi.Services.Password
{
    public interface IPasswordInterface
    {
        void CreateHashPassword(string password, out byte[] hashPassword, out byte[] saltPassword);
        bool VerifyHashPassword(string password, byte[] hashPassword, byte[] saltPassword);
        string CreateToken(UserModel user);
    }
}
