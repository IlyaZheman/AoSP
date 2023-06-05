using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IGradeService
{
    Task<BaseResponse<GradeViewModel>> GetGrade(int selectedGroupId);
}