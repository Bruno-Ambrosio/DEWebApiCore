using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;

namespace DoctorEaseWebApi.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ResponseModel<UserModel>> Register(CreateUserDto createUserDto);
        Task<ResponseModel<string>> Login(LoginDto loginDto);
    }
}
