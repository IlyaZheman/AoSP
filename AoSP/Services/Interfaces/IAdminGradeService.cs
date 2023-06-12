using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels.Admin;

namespace AoSP.Services.Interfaces;

public interface IAdminGradeService
{
    Task<BaseResponse<GradeViewModel>> Get(string? selectedGroupId = null);
    Task<BaseResponse<Group>> Create(GroupViewModel model);
}