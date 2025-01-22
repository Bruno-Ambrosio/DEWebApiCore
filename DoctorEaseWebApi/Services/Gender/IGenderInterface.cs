using DEWebApi.Models;
using DoctorEaseWebApi.Models;

namespace DEWebApi.Services.Gender
{
    public interface IGenderInterface
    {
        Task<ResponseModel<List<GenderModel>>> GetGenders();
    }
}
