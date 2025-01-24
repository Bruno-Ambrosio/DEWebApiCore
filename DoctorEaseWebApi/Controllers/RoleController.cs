using DEWebApi.Dto.Patient;
using DEWebApi.Dto.Role;
using DEWebApi.Models;
using DEWebApi.Services.Patient;
using DEWebApi.Services.Role;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleInterface _roleInterface;

        public RoleController(IRoleInterface roleInterface)
        {
            _roleInterface = roleInterface;
        }


        [HttpGet("Roles")]
        public async Task<ActionResult<ResponseModel<List<RoleModel>>>> GetPatients()
        {
            ResponseModel<List<RoleModel>> response = await _roleInterface.GetRoles();
            return Ok(response);
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<ResponseModel<RoleModel>>> CreateRole(CreateRoleDto createRoleDto)
        {
            ResponseModel<RoleModel> response = await _roleInterface.CreateRole(createRoleDto);
            return Ok(response);
        }
    }
}
