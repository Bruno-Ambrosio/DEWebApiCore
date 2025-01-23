using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Password;
using DoctorEaseWebApi.Static.Messages;
using Microsoft.EntityFrameworkCore;

namespace DoctorEaseWebApi.Services.User
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _DbContext;
        private readonly IPasswordInterface _PasswordInterface;

        public UserService(AppDbContext dbContext, IPasswordInterface passwordInterface)
        {
            _DbContext = dbContext;
            _PasswordInterface = passwordInterface;
        }

        public async Task<ResponseModel<List<UserModel>>> EditUser(EditUserDto editUserDto)
        {
            ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();

            try
            {
                UserModel? user = await _DbContext.Users.FirstOrDefaultAsync(user => user.Id == editUserDto.Id);

                if (user == null)
                {
                    response.Message = "User not found!";
                    return response;
                }

                _PasswordInterface.CreateHashPassword(editUserDto.Password, out byte[] hashPassword, out byte[] saltPassword);

                user.Name = editUserDto.Name;
                user.Email = editUserDto.Email;
                user.HashPassword = hashPassword;
                user.SaltPassword = saltPassword;

                _DbContext.Update(user);
                await _DbContext.SaveChangesAsync();

                response.Content = await _DbContext.Users.ToListAsync();
                response.Message = "User succesfully edited!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<UserModel>> GetUserByEmail(string userEmail)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                UserModel? user = await _DbContext.Users.FirstOrDefaultAsync(user => user.Email == userEmail);

                if (user == null)
                {
                    response.Message = "User not found!";
                    return response;
                }

                response.Content = user;
                response.Message = "User found!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<UserModel>> GetUserById(int idUser)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                UserModel? user = await _DbContext.Users.FirstOrDefaultAsync(user => user.Id == idUser);

                if (user == null)
                {
                    response.Message = WarningMessages.UserNotFound;
                    return response;
                }

                response.Content = user;
                response.Message = SuccessMessages.UserFound;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<UserModel>>> GetUsers()
        {
            ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();

            try
            {
                List<UserModel> users = await _DbContext.Users.ToListAsync();

                response.Content = users;
                response.Message = "Get all users succeded!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<UserModel>>> RemoveUser(int userId)
        {
            ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();

            try
            {
                UserModel? user = await _DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    response.Message = "User not found!";
                    return response;
                }

                _DbContext.Users.Remove(user);
                await _DbContext.SaveChangesAsync();

                response.Content = await _DbContext.Users.ToListAsync();
                response.Message = "User succesfully deleted!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
    }
}
