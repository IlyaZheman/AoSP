﻿using AoSP.Entities;
using AoSP.Enums;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IBaseRepository<Profile> _profileRepository;
    private readonly IBaseRepository<User> _userRepository;

    public UserService(ILogger<UserService> logger,
                       IBaseRepository<User> userRepository,
                       IBaseRepository<Profile> profileRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _profileRepository = profileRepository;
    }

    public async Task<IBaseResponse<User>> Create(UserViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
            if (user != null)
            {
                return new BaseResponse<User>()
                {
                    Description = "Пользователь с таким логином уже есть",
                    StatusCode = StatusCode.UserAlreadyExists
                };
            }

            user = new User()
            {
                Login = model.Login,
                Role = model.Role,
                // Password = HashPasswordHelper.HashPassowrd(model.Password),
            };

            await _userRepository.Create(user);

            var profile = new Profile()
            {
                UserId = user.Id,
            };

            await _profileRepository.Create(profile);

            return new BaseResponse<User>()
            {
                Data = user,
                Description = "Пользователь добавлен",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
            return new BaseResponse<User>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAll()
                                             .Select(x => new UserViewModel
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name,
                                                 Surname = x.Surname,
                                                 Patronymic = x.Patronymic,
                                                 Login = x.Login,
                                                 Role = x.Role
                                             })
                                             .ToListAsync();

            _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
            return new BaseResponse<IEnumerable<UserViewModel>>()
            {
                Data = users,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UserService.GetUsers] error: {ex.Message}");
            return new BaseResponse<IEnumerable<UserViewModel>>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteUser(long id)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.UserNotFound,
                    Data = false
                };
            }

            await _userRepository.Delete(user);
            _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Ok,
                Data = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<User>> Edit(int id, UserViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll()
                                            .Where(x => x.Login == model.Login)
                                            .Where(x => x.Id != model.Id)
                                            .FirstOrDefaultAsync();
            if (user != null)
            {
                return new BaseResponse<User>()
                {
                    Description = "This login is already in use",
                    StatusCode = StatusCode.UserNotFound
                };
            }
            
            user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (user == null)
            {
                return new BaseResponse<User>()
                {
                    Description = "User not found",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Patronymic = model.Patronymic;
            user.Login = model.Login;

            await _userRepository.Update(user);

            return new BaseResponse<User>()
            {
                Data = user,
                StatusCode = StatusCode.Ok,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<UserViewModel>> GetUser(int id)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = "Пользователь не найден",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var data = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Login = user.Login,
                Role = user.Role
            };

            return new BaseResponse<UserViewModel>()
            {
                StatusCode = StatusCode.Ok,
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<UserViewModel>()
            {
                Description = $"[GetCar] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}