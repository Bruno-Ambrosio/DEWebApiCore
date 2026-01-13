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
    }
}
