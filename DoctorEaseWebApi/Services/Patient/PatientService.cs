using DEWebApi.Dto.Patient;
using DEWebApi.Models;
using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Password;
using Microsoft.EntityFrameworkCore;

namespace DEWebApi.Services.Patient
{
    public class PatientService : IPatientInterface
    {
        private readonly AppDbContext _DbContext;
        private readonly IConfiguration _Configuration;

        public PatientService(AppDbContext dbContext, IConfiguration configuration)
        {
            _DbContext = dbContext;
            _Configuration = configuration;
        }

        public async Task<ResponseModel<List<PatientModel>>> GetPatients()
        {
            ResponseModel<List<PatientModel>> response = new ResponseModel<List<PatientModel>>();

            try
            {
                List<PatientModel> patients = await _DbContext.Patients.ToListAsync();

                response.Content = patients;
                response.Message = "Get all patients succeded!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<PatientModel>> CreatePatient(CreatePatientDto createPatientDto)
        {
            ResponseModel<PatientModel> response = new ResponseModel<PatientModel>();

            try
            {
                GenderModel? gender = await _DbContext.Genders.FirstOrDefaultAsync(gender => gender.Id == createPatientDto.GenderId);

                if (gender == null)
                {
                    response.Message = "Gender not found!";
                    response.Success = false;
                    return response;
                }

                PatientModel user = new PatientModel()
                {
                    Name = createPatientDto.Name,
                    Adress = createPatientDto.Adress,
                    Gender = gender,
                };

                _DbContext.Add(user);
                await _DbContext.SaveChangesAsync();

                response.Message = "Patient succesfully created!";
                response.Content = user;
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
