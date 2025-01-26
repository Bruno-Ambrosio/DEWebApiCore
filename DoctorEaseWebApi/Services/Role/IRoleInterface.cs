using DEWebApi.Dto.Role;
using DEWebApi.Models;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;

namespace DEWebApi.Services.Role
{
    public interface IRoleInterface
    {
        Task<ResponseModel<List<RoleModel>>> GetRoles();
        Task<ResponseModel<RoleModel>> CreateRole(CreateRoleDto createRoleDto);
        Task<ResponseModel<List<RoleModel>>> EditRole(EditRoleDto editRoleDto);
    }
}
