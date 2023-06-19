using AoSP.Response;
using AoSP.ViewModels.Teacher;

namespace AoSP.Services.Interfaces;

public interface ITeacherGradeService
{
    Task<BaseResponse<TeacherGradeViewModel>> Get(string userName);
    Task<BaseResponse<bool>> SetScore(string id, int score);
    Task<BaseResponse<bool>> DownloadFile(string id);
}