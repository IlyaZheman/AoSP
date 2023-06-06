using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IGradeService
{
    Task<BaseResponse<GradeViewModel>> Get(int selectedGroupId);
    Task<BaseResponse<Group>> Create(GroupViewModel model);
}