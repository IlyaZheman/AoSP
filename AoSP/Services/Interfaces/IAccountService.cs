using System.Security.Claims;
using AoSP.Response;
using AoSP.ViewModels.Account;

namespace AoSP.Services.Interfaces;

public interface IAccountService
{
    Task<IBaseResponse<LoginViewModel>> Login(LoginViewModel model);

    Task<IBaseResponse<bool>> HasPassword(LoginViewModel model);

    Task<IBaseResponse<ClaimsIdentity>> EnterPassword(PasswordViewModel model);

    Task<IBaseResponse<ClaimsIdentity>> CreatePassword(CreatePasswordViewModel model);

    Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
}