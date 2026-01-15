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

        public async Task<ResponseModel<bool>> UploadImage(IFormFile file, int userId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                if (file == null || file.Length == 0)
                {
                    response.Content = false;
                    response.Message = $"Invalid file.";
                    response.Success = false;
                    return response;
                }

                string? ext = Path.GetExtension(file.FileName).ToLower();
                string? fileNameGuid = $"{userId}{ext}";
                string? path = Path.Combine("Uploads", "User", fileNameGuid);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                using FileStream stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                UserModel? user = await _DbContext.Users.FindAsync(userId);

                if (user == null)
                {
                    response.Content = false;
                    response.Message = "User not found.";
                    response.Success = false;
                    return response;
                }

                user.ImagePath = path;
                await _DbContext.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<string> GetImagePath(int userId)
        {
            UserModel? user = await _DbContext.Users.FindAsync(userId);
            return user?.ImagePath ?? string.Empty;
        }

        public async Task<ResponseModel<bool>> DeleteImage(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                UserModel? user = await _DbContext.Users.FindAsync(id);

                if (user == null)
                {
                    response.Content = false;
                    response.Message = "User not found.";
                    response.Success = true;
                    return response;
                }

                if (File.Exists(user.ImagePath))
                {
                    File.Delete(user.ImagePath);
                }

                user.ImagePath = null;
                await _DbContext.SaveChangesAsync();

                response.Content = true;
                response.Message = "Image deleted!";
                response.Success = true;
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
