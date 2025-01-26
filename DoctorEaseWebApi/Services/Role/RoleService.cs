using DEWebApi.Dto.Role;
using DEWebApi.Models;
using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Password;
using Microsoft.EntityFrameworkCore;

namespace DEWebApi.Services.Role
{
    public class RoleService : IRoleInterface
    {
        private readonly AppDbContext _DbContext;
        private readonly IRoleInterface _IRoleInterface;

        public RoleService(AppDbContext dbContext, IRoleInterface IRoleInterface)
        {
            _DbContext = dbContext;
            _IRoleInterface = IRoleInterface;
        }

        public async Task<ResponseModel<List<RoleModel>>> GetRoles()
        {
            ResponseModel<List<RoleModel>> response = new ResponseModel<List<RoleModel>>();

            try
            {
               
                List<RoleModel>? roles = await _DbContext.Roles.ToListAsync();

                if (roles == null)
                {
                    response.Message = "No roles found!";
                    response.Success = false;
                    return response;
                }

                response.Message = "User succesfully registered!";
                response.Content = roles;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<RoleModel>> CreateRole(CreateRoleDto createRoleDto)
        {
            ResponseModel<RoleModel> response = new ResponseModel<RoleModel>();

            try
            {
                RoleModel role = new RoleModel()
                {
                    Description = createRoleDto.Description,
                };

                _DbContext.Add(role);
                await _DbContext.SaveChangesAsync();

                response.Message = "Role succesfully created!";
                response.Content = role;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<RoleModel>>> EditRole(EditRoleDto editRoleDto)
        {
            ResponseModel<List<RoleModel>> response = new ResponseModel<List<RoleModel>>();

            try
            {
                RoleModel? role = await _DbContext.Roles.FirstOrDefaultAsync(user => user.Id == editRoleDto.Id);

                if (role == null)
                {
                    response.Message = "Role not found!";
                    return response;
                }

                role.Description = editRoleDto.Description;

                _DbContext.Update(role);
                await _DbContext.SaveChangesAsync();

                response.Content = await _DbContext.Roles.ToListAsync();
                response.Message = "Role succesfully edited!";

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
