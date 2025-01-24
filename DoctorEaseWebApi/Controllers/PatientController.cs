using DEWebApi.Dto.Patient;
using DEWebApi.Models;
using DEWebApi.Services.Patient;
using DoctorEaseWebApi.Dto.Auth;
using DoctorEaseWebApi.Dto.User;
using DoctorEaseWebApi.Models;
using DoctorEaseWebApi.Services.Auth;
using DoctorEaseWebApi.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace DEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientInterface _patientInterface;

        public PatientController(IPatientInterface patientInterface)
        {
            _patientInterface = patientInterface;
        }


        [HttpGet("Patients")]
        public async Task<ActionResult<ResponseModel<List<PatientModel>>>> GetPatients()
        {
            ResponseModel<List<PatientModel>> response = await _patientInterface.GetPatients();
            return Ok(response);
        }

        [HttpPost("CreatePatient")]
        public async Task<ActionResult<ResponseModel<PatientModel>>> CreatePatient(CreatePatientDto createPatientDto)
        {
            ResponseModel<PatientModel> response = await _patientInterface.CreatePatient(createPatientDto);
            return Ok(response);
        }
    }
}
