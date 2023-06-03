using System.Security.Claims;
using AoSP.Entities;
using AoSP.Enums;
using AoSP.Helpers;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IBaseRepository<User> _userRepository;

    public AccountService(ILogger<AccountService> logger,
        IBaseRepository<User> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IBaseResponse<LoginViewModel>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
            if (user == null)
            {
                return new BaseResponse<LoginViewModel>
                {
                    Description = "Пользователь не найден"
                };
            }

            return new BaseResponse<LoginViewModel>
            {
                Data = model,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<LoginViewModel>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> HasPassword(LoginViewModel model)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
        if (user == null)
        {
            return new BaseResponse<bool>
            {
                Description = "Пользователь не найден"
            };
        }

        return new BaseResponse<bool>
        {
            Data = !string.IsNullOrWhiteSpace(user.Password),
            StatusCode = StatusCode.Ok
        };
    }

    public async Task<IBaseResponse<ClaimsIdentity>> Password(PasswordViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);

            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Пользователь не найден"
                };
            }

            if (user.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Неверный пароль"
                };
            }

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<ClaimsIdentity>> CreatePassword(CreatePasswordViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь не найден"
                };
            }

            user.Password = HashPasswordHelper.HashPassword(model.Password);
            await _userRepository.Update(user);

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Register]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.UserLogin);
            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "Пользователь не найден"
                };
            }

            user.Password = HashPasswordHelper.HashPassword(model.NewPassword);
            await _userRepository.Update(user);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.Ok,
                Description = "Пароль обновлен"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private static ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}