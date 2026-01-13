using DEWebApi.Models;
using DoctorEaseWebApi.Data;
using DoctorEaseWebApi.Models;
using Microsoft.AspNetCore.Mvc;

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
                        FileName = fileName,
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
    }
}
