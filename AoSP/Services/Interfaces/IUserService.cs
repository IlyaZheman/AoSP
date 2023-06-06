using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<User>> Create(UserViewModel model);
    Task<BaseResponse<IEnumerable<UserViewModel>>> GetAll();
    Task<IBaseResponse<UserViewModel>> Get(int id);
    Task<IBaseResponse<UserViewModel>> Get(string login);
    Task<IBaseResponse<UserViewModel>> Edit(int id, UserViewModel model);
    Task<IBaseResponse<bool>> Delete(long id);
}