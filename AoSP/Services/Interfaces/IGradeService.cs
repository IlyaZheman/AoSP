using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IGradeService
{
    Task<BaseResponse<GradeViewModel>> Get(string? selectedGroupId = null);
    Task<BaseResponse<Group>> Create(GroupViewModel model);
}