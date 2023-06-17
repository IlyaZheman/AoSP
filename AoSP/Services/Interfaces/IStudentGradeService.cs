using AoSP.Response;
using AoSP.ViewModels.Student;

namespace AoSP.Services.Interfaces;

public interface IStudentGradeService
{
    Task<BaseResponse<StudentGradeViewModel>> Get(string userName);
    Task<BaseResponse<bool>> UploadFile(PersonalSubjectTaskViewModel model);
    Task<BaseResponse<bool>> DownloadFile(string id);
}