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

        public async Task<ResponseModel<List<RoleModel>>> GetAllRoles()
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
    }
}
