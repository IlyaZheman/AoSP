using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IGroupService
{
    Task<BaseResponse<IEnumerable<GroupViewModel>>> GetAllGroups();
}