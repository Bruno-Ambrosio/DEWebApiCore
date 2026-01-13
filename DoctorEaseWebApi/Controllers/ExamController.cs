using DEWebApi.Dto.Exam;
using DEWebApi.Services.Exam;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamInterface _examInterface;

        public ExamController(IExamInterface examInterface)
        {
            _examInterface = examInterface;
        }

        [HttpPost("UploadExam")]
        public async Task<ActionResult<ResponseModel<bool>>> UploadExam(
                                             [FromForm] List<IFormFile> files,
                                             [FromForm] List<string> titles,
                                             [FromForm] List<string> fileNames,
                                             [FromForm] List<string> dates,
                                             [FromForm] List<int> patientIds)
        {
            ResponseModel<bool> response = await _examInterface.UploadExam(files, titles, fileNames, dates, patientIds);
            return Ok(response);
        }

        [HttpGet("GetExamsByPatientId:{id}")]
        public async Task<ActionResult<ResponseModel<List<GetExamsByPatientIdDto>>>> GetExamsByPatientId(int id)
        {
            ResponseModel<List<GetExamsByPatientIdDto>> response = await _examInterface.GetExamsByPatientId(id);
            return Ok(response);
        }

        [HttpGet("Open/{id}")]
        public async Task<IActionResult> OpenExam(int id)
        {
            var result = await _examInterface.GetExamFile(id);

            if (result == null)
            {
                return NotFound();
            }

            var (filePath, fileName) = result.Value;

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(stream, "application/pdf");
        }

        [HttpDelete("Delete:{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteExam(int id)
        {
            ResponseModel<bool> response = await _examInterface.DeleteExam(id);
            return Ok(response);
        }
    }
}
