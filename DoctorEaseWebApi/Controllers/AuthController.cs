using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorEaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _AuthInterface;

        public AuthController(IAuthInterface authInterface)
        {
            _AuthInterface = authInterface;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<string>>> Login(LoginDto loginDto)
        {
            ResponseModel<string> response = await _AuthInterface.Login(loginDto);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseModel<UserModel>>> Register(CreateUserDto createUserDto)
        {
            ResponseModel<UserModel> response = await _AuthInterface.Register(createUserDto);
            return Ok(response);
        }
    }
}
