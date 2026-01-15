using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;

namespace DoctorEaseWebApi.Services.User
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<UserModel>>> GetUsers();
        Task<ResponseModel<UserModel>> GetUserById(int idUser);
        Task<ResponseModel<UserModel>> GetUserByEmail(string userEmail);
        Task<ResponseModel<List<UserModel>>> EditUser(EditUserDto editUserDto);
        Task<ResponseModel<List<UserModel>>> RemoveUser(int userId);
        Task<ResponseModel<bool>> UploadImage(IFormFile file, int userId);
        Task<string> GetImagePath(int userId);
        Task<ResponseModel<bool>> DeleteImage(int id);
    }
}
