using DEWebApi.Dto.Exam;
using DEWebApi.Models;
using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEWebApi.Services.Exam
{
    public class ExamService : IExamInterface
    {
        private readonly AppDbContext _DbContext;
        private readonly IConfiguration _Configuration;

        public ExamService(AppDbContext dbContext, IConfiguration configuration)
        {
            _DbContext = dbContext;
            _Configuration = configuration;
        }

        public async Task<ResponseModel<bool>> UploadExam(
                                             [FromForm] List<IFormFile> files,
                                             [FromForm] List<string> titles,
                                             [FromForm] List<string> fileNames,
                                             [FromForm] List<string> dates,
                                             [FromForm] List<int> patientIds)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {

                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    var title = titles[i];
                    var fileName = fileNames[i];
                    var date = DateTime.Parse(dates[i]);
                    var patientId = patientIds[i];

                    if (file == null || file.Length == 0)
                    {
                        response.Content = false;
                        response.Message = $"Invalid file: {fileName}.";
                        response.Success = false;
                        return response;
                    }

                    var ext = Path.GetExtension(fileName).ToLower();
                    var fileNameGuid = $"{Guid.NewGuid()}{ext}";
                    var path = Path.Combine("Uploads", "Exams", fileNameGuid);

                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    var newExam = new ExamModel
                    {
                        Title = title,
                        FileName = fileNameGuid,
                        FilePath = path,
                        Date = date.ToUniversalTime(),
                        PatientId = patientId
                    };
                    await _DbContext.Exams.AddAsync(newExam);
                }

                if (await _DbContext.SaveChangesAsync() > 0)
                {
                    response.Content = true;
                    response.Message = "Exams uploaded!";
                    response.Success = true;
                }
                else
                {
                    response.Content = false;
                    response.Message = "Failed to upload exams.";
                    response.Success = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<GetExamsByPatientIdDto>>> GetExamsByPatientId(int patientId)
        {
            ResponseModel<List<GetExamsByPatientIdDto>> response = new ResponseModel<List<GetExamsByPatientIdDto>>();

            try
            {
                List<ExamModel> exams = await _DbContext.Exams.Where(exam => exam.PatientId == patientId).ToListAsync();

                if (exams == null || exams.Count == 0)
                {
                    response.Content = null;
                    response.Message = "Patient doesn't have any exam.";
                    response.Success = true;
                    return response;
                }

                List<GetExamsByPatientIdDto> examsDto = new List<GetExamsByPatientIdDto>();

                foreach (var exam in exams)
                {
                    examsDto.Add(new GetExamsByPatientIdDto
                    {
                        Id = exam.Id,
                        Title = exam.Title
                    });
                }

                response.Content = examsDto;
                response.Message = "Get exams succeded!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        public async Task<ExamModel?> GetExamFile(int id)
        {
            var exam = await _DbContext.Exams.FindAsync(id);

            if (exam == null)
                return null;

            if (!System.IO.File.Exists(exam.FilePath))
                return null;

            return (exam);
        }

        public async Task<ResponseModel<bool>> DeleteExam(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                ExamModel? exam = await _DbContext.Exams.FindAsync(id);

                if (exam == null)
                {
                    response.Content = false;
                    response.Message = "Exam not found.";
                    response.Success = true;
                    return response;
                }

                if (File.Exists(exam.FilePath))
                {
                    File.Delete(exam.FilePath);
                }

                _DbContext.Exams.Remove(exam);
                await _DbContext.SaveChangesAsync();

                response.Content = true;
                response.Message = "Exam deleted!";
                response.Success = true;
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
