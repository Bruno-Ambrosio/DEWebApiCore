using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorEaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [Authorize]
        [HttpGet("Users")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetUsers()
        {
            ResponseModel<List<UserModel>> response = await _userInterface.GetUsers();
            return Ok(response);
        }

        [Authorize]
        [HttpPut("EditUser")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> EditUser(EditUserDto editUserDto)
        {
            ResponseModel<List<UserModel>> response = await _userInterface.EditUser(editUserDto);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("RemoveUser")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> RemoveUser(int userId)
        {
            ResponseModel<List<UserModel>> response = await _userInterface.RemoveUser(userId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetUserById(int userId)
        {
            ResponseModel<UserModel> response = await _userInterface.GetUserById(userId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("UploadImage")]
        public async Task<ActionResult<ResponseModel<bool>>> UploadImage(IFormFile file)
        {
            int id = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            ResponseModel<bool> response = await _userInterface.UploadImage(file, id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetImage")]
        public async Task<ActionResult<ResponseModel<bool>>> GetImage()
        {
            int id = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            string imagePath = await _userInterface.GetImagePath(id);
            if (imagePath == null || !System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            return PhysicalFile(imagePath, "image/jpeg");
        }

        [Authorize]
        [HttpDelete("DeleteImage")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteImage()
        {
            int id = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            ResponseModel<bool> response = await _userInterface.DeleteImage(id);
            return Ok(response);
        }
    }
}
