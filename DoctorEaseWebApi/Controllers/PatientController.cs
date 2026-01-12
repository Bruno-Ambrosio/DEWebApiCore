using DEWebApi.Dto.Patient;
using DEWebApi.Models;
using DEWebApi.Services.Patient;
using DoctorEaseWebApi.Models;
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

        [HttpGet("GetPatient:{id}")]
        public async Task<ActionResult<ResponseModel<PatientModel>>> GetPatientById(int id)
        {
            ResponseModel<PatientModel> response = await _patientInterface.GetPatientById(id);
            return Ok(response);
        }

        [HttpPut("EditPatient")]
        public async Task<ActionResult<ResponseModel<PatientModel>>> EditPatient(EditPatientDto patient)
        {
            ResponseModel<PatientModel> response = await _patientInterface.EditPatient(patient);
            return Ok(response);
        }

        [HttpPatch("ChangePatientStatus:{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> ChangePatientStatus(int id, [FromBody] bool active)
        {
            ResponseModel<bool> response = await _patientInterface.ChangePatientStatus(id, active);
            return Ok(response);
        }

        [HttpPatch("UpdateAdditionalInfo:{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> UpdateAdditionalInfo(int id, [FromBody] string info)
        {
            ResponseModel<bool> response = await _patientInterface.UpdateAdditionalInfo(id, info);
            return Ok(response);
        }
    }
}
