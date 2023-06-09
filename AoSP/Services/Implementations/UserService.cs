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
    private readonly IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
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

            user = new User
            {
                Login = model.Login,
                Role = model.Role ?? Role.Student,
            };

            await _userRepository.Create(user);

            return new BaseResponse<User>()
            {
                Data = user,
                Description = "Пользователь добавлен",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetAll()
    {
        try
        {
            var users = await _userRepository.GetAll().ToListAsync();

            var views = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Login = user.Login,
                Role = user.Role
            });

            return new BaseResponse<IEnumerable<UserViewModel>>()
            {
                Data = views,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<UserViewModel>>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(string id)
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

            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Ok,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<UserViewModel>> Edit(UserViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll()
                .Where(x => x.Login == model.Login)
                .Where(x => x.Id != model.Id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = "This login is already in use",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (user == null)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = "User not found",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            user.Login = model.Login;
            user.Role = model.Role ?? Role.Student;

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Patronymic = model.Patronymic;

            await _userRepository.Update(user);

            return new BaseResponse<UserViewModel>()
            {
                Data = model,
                StatusCode = StatusCode.Ok,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<UserViewModel>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<UserViewModel>> Get(string id)
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
                Description = $"[GetUser] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<UserViewModel>> GetByUserName(string login)
    {
        try
        {
            var user = await _userRepository.GetAll()
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    Role = x.Role,
                    Login = x.Login
                })
                .FirstOrDefaultAsync(x => x.Login == login);

            return new BaseResponse<UserViewModel>()
            {
                Data = user,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<UserViewModel>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}