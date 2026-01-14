using DEWebApi.Dto.Exam;
using DEWebApi.Services.Exam;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpGet("GetExamsByPatientId:{id}")]
        public async Task<ActionResult<ResponseModel<List<GetExamsByPatientIdDto>>>> GetExamsByPatientId(int id)
        {
            ResponseModel<List<GetExamsByPatientIdDto>> response = await _examInterface.GetExamsByPatientId(id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Open/{id}")]
        public async Task<IActionResult> OpenExam(int id)
        {
            var exam = await _examInterface.GetExamFile(id);

            if (exam == null)
            {
                return NotFound();
            }

            var stream = new FileStream(exam.FilePath, FileMode.Open, FileAccess.Read);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{exam.FileName}\"";

            return File(stream, "application/pdf", exam.Title);
        }

        [Authorize]
        [HttpDelete("Delete:{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteExam(int id)
        {
            ResponseModel<bool> response = await _examInterface.DeleteExam(id);
            return Ok(response);
        }
    }
}
