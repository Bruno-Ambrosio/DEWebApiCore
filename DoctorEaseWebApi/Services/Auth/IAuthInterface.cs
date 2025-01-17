using DoctorEaseWebApi.Dto.Auth;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;

namespace DoctorEaseWebApi.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ResponseModel<UserModel>> Register(CreateUserDto createUserDto);
        Task<ResponseModel<AuthDto>> Login(LoginDto loginDto);
    }
}
