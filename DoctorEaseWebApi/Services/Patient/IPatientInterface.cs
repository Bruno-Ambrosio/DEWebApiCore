using DEWebApi.Dto.Patient;
using DEWebApi.Models;
using DoctorEaseWebApi.Models;

namespace DEWebApi.Services.Patient
{
    public interface IPatientInterface
    {
        Task<ResponseModel<List<PatientModel>>> GetPatients();
        Task<ResponseModel<PatientModel>> CreatePatient(CreatePatientDto createPatientDto);
        Task<ResponseModel<PatientModel>> GetPatientById(int patientId);
        Task<ResponseModel<PatientModel>> EditPatient(EditPatientDto patient);
        Task<ResponseModel<bool>> ChangePatientStatus(int id, bool active);
        Task<ResponseModel<bool>> UpdateAdditionalInfo(int id, string info);
    }
}
