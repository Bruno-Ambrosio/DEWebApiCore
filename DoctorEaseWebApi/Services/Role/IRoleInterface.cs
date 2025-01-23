using DEWebApi.Dto.Role;
using DEWebApi.Models;
using DoctorEaseWebApi.Models;

namespace DEWebApi.Services.Role
{
    public interface IRoleInterface
    {
        Task<ResponseModel<List<RoleModel>>> GetAllRoles();
    }
}
