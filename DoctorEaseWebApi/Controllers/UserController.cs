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
    }
}
