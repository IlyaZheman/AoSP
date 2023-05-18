using System.Security.Claims;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces;

public interface IAccountService
{
    Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

    Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

    Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
}