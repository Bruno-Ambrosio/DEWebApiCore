using DEWebApi.Dto.Exam;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEWebApi.Services.Exam
{
    public interface IExamInterface
    {
        Task<ResponseModel<bool>> UploadExam([FromForm] List<IFormFile> files,
                                             [FromForm] List<string> titles,
                                             [FromForm] List<string> fileNames,
                                             [FromForm] List<string> dates,
                                             [FromForm] List<int> patientIds);
        Task<ResponseModel<List<GetExamsByPatientIdDto>>> GetExamsByPatientId(int patientId);
        Task<(string FilePath, string FileName)?> GetExamFile(int id);
        Task<ResponseModel<bool>> DeleteExam(int id);
    }
}
