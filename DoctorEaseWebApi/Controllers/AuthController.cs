using DEWebApi.Dto.User;
using DoctorEaseWebApi.Dto.Auth;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Auth;
using DoctorEaseWebApi.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorEaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _AuthInterface;
        private readonly IUserInterface _userInterface;

        public AuthController(IAuthInterface authInterface, IUserInterface userInterface)
        {
            _AuthInterface = authInterface;
            _userInterface = userInterface;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<AuthDto>>> Login(LoginDto loginDto)
        {
            ResponseModel<AuthDto> response = await _AuthInterface.Login(loginDto);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Register(CreateUserDto createUserDto)
        {
            ResponseModel<UserModel> response = await _AuthInterface.Register(createUserDto);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<ActionResult<ResponseModel<LoggedUserDto>>> Me()
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            var user = await _userInterface.GetUserById(userId);
            var response = new ResponseModel<LoggedUserDto>
            {
                Content = new LoggedUserDto
                {
                    Id = user.Content!.Id,
                    Name = user.Content.Name,
                    Email = user.Content.Email,
                    RoleId = user.Content.RoleId,
                },

                Message = "User validated.",
                Success = true
            };

            return Ok(response);
        }
    }
}
