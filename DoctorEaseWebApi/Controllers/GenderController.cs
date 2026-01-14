using DEWebApi.Models;
using DEWebApi.Services.Gender;
using DEWebApi.Services.Patient;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderInterface _genderInterface;

        public GenderController(IGenderInterface genderInterface)
        {
            _genderInterface = genderInterface;
        }

        [Authorize]
        [HttpGet("Genders")]
        public async Task<ActionResult<ResponseModel<List<GenderModel>>>> GetGenders()
        {
            ResponseModel<List<GenderModel>> response = await _genderInterface.GetGenders();
            return Ok(response);
        }
    }
}
