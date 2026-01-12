using DEWebApi.Dto.Patient;
using DEWebApi.Models;
using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Models;
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

        public async Task<ResponseModel<PatientModel>> GetPatientById(int patientId)
        {
            ResponseModel<PatientModel> response = new ResponseModel<PatientModel>();

            try
            {
                PatientModel? patient = await _DbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == patientId);

                if (patient == null)
                {
                    response.Message = "Patient not found!";
                    response.Success = false;
                    return response;
                }

                GenderModel? gender = await _DbContext.Genders.FirstOrDefaultAsync(gender => gender.Id == patient.GenderId);

                if (gender == null)
                {
                    response.Message = $"Gender of the patient id: {patient.Id} not found!";
                    response.Success = false;
                    return response;
                }

                response.Content = patient;
                response.Message = $"Get patient {patient.Name} succeded!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<PatientModel>> EditPatient(EditPatientDto patient)
        {
            ResponseModel<PatientModel> response = new ResponseModel<PatientModel>();

            try
            {
                PatientModel? originalPatient = await _DbContext.Patients.FirstOrDefaultAsync(original => original.Id == patient.Id);

                if (originalPatient == null)
                {
                    response.Message = "Patient not found!";
                    response.Success = false;
                    return response;
                }

                GenderModel? gender = await _DbContext.Genders.FirstOrDefaultAsync(gender => gender.Id == patient.GenderId);

                if (gender == null)
                {
                    response.Message = $"Gender of the patient id: {originalPatient.Id} not found!";
                    response.Success = false;
                    return response;
                }

                originalPatient.Name = patient.Name;
                originalPatient.Adress = patient.Adress;
                originalPatient.GenderId = patient.GenderId;

                await _DbContext.SaveChangesAsync();

                response.Content = originalPatient;
                response.Message = $"Get patient {patient.Name} succeded!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<bool>> ChangePatientStatus(int id, bool active)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                PatientModel? patient = await _DbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);

                if (patient == null)
                {
                    response.Message = "Patient not found!";
                    response.Success = false;
                    return response;
                }

                patient.Active = active;

                await _DbContext.SaveChangesAsync();

                response.Content = true;
                response.Message = $"Patient {patient.Name} active set to {patient.Active} succeded!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<bool>> UpdateAdditionalInfo(int id, string info)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                PatientModel? patient = await _DbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);

                if (patient == null)
                {
                    response.Message = "Patient not found!";
                    response.Success = false;
                    return response;
                }

                patient.AdditionalInfo = info;

                await _DbContext.SaveChangesAsync();

                response.Content = true;
                response.Message = $"Patient {patient.Name} additional info updated!";

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
