using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Dto;
using DoctorEaseWebApi.Dto.Auth;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Password;
using DoctorEaseWebApi.Static.Messages;
using Microsoft.EntityFrameworkCore;

namespace DoctorEaseWebApi.Services.Auth
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _DbContext;
        private readonly IPasswordInterface _PasswordInterface;

        public AuthService(AppDbContext dbContext, IPasswordInterface passwordInterface)
        {
            _DbContext = dbContext;
            _PasswordInterface = passwordInterface;
        }
        public async Task<ResponseModel<UserModel>> Register(CreateUserDto createUserDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                if (VerifyEmailAlreadyExist(createUserDto))
                {
                    response.Message = "Email already in use!";
                    response.Success = false;
                    return response;
                }

                _PasswordInterface.CreateHashPassword(createUserDto.Password, out byte[] hashPassword, out byte[] saltPassword);

                UserModel user = new UserModel()
                {
                    Name = createUserDto.Name,
                    Email = createUserDto.Email,
                    Role = createUserDto.Role,
                    HashPassword = hashPassword,
                    SaltPassword = saltPassword,
                };

                _DbContext.Add(user);
                await _DbContext.SaveChangesAsync();

                response.Message = "User succesfully registered!";
                response.Content = user;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<AuthDto>> Login(LoginDto loginDto)
        {
            ResponseModel<AuthDto> response = new ResponseModel<AuthDto>();

            try
            {
                UserModel? user = await _DbContext.Users.FirstOrDefaultAsync(user => user.Email == loginDto.Email);

                if (user == null)
                {
                    response.Message = WarningMessages.EmailDoesntExist;
                    response.Success = false;
                    return response;
                }

                if (!_PasswordInterface.VerifyHashPassword(loginDto.Password, user.HashPassword, user.SaltPassword))
                {
                    response.Message = WarningMessages.InvalidPassword;
                    response.Success = false;
                    return response;
                }

                string token = _PasswordInterface.CreateToken(user);

                response.Message = SuccessMessages.SuccesfullyLogged;

                response.Content = new AuthDto() 
                { 
                    Token = token,
                    User = user,
                };

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public bool VerifyEmailAlreadyExist(CreateUserDto createUserDto)
        {
            UserModel? user = _DbContext.Users.FirstOrDefault(user => user.Email == createUserDto.Email);

            return user != null;
        }
    }
}
