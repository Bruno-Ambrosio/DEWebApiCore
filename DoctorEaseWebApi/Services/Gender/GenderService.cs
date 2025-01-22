using DEWebApi.Models;
using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Static.Messages;
using Microsoft.EntityFrameworkCore;

namespace DEWebApi.Services.Gender
{
    public class GenderService : IGenderInterface
    {
        private readonly AppDbContext _DbContext;
        private readonly IConfiguration _Configuration;

        public GenderService(AppDbContext dbContext, IConfiguration configuration)
        {
            _DbContext = dbContext;
            _Configuration = configuration;
        }


        public async Task<ResponseModel<List<GenderModel>>> GetGenders()
        {
            ResponseModel<List<GenderModel>> response = new ResponseModel<List<GenderModel>>();

            try
            {
                List<GenderModel> genders = await _DbContext.Genders.ToListAsync();

                response.Content = genders;
                response.Message = "Get all genders succeded!";

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
