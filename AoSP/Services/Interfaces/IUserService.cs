using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<User>> Create(UserViewModel model);
    Task<BaseResponse<IEnumerable<UserViewModel>>> GetAll();
    Task<IBaseResponse<UserViewModel>> Get(string id);
    Task<IBaseResponse<UserViewModel>> GetByUserName(string login);
    Task<IBaseResponse<UserViewModel>> Edit(UserViewModel model);
    Task<IBaseResponse<bool>> Delete(string id);
}