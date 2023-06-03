using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<User>> Create(UserViewModel model);
    Task<BaseResponse<IEnumerable<UserViewModel>>> GetAllUsers();
    Task<IBaseResponse<bool>> DeleteUser(long id);
    Task<IBaseResponse<UserViewModel>> Edit(int id, UserViewModel model);
    Task<IBaseResponse<UserViewModel>> GetUser(int id);
    Task<IBaseResponse<ProfileViewModel>> GetProfile(string login);
}