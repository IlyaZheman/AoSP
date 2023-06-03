using AoSP.Entities;
using AoSP.Enums;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class GroupService : IGroupService
{
    private readonly ILogger<UserService> _logger;
    private readonly IBaseRepository<Group> _groupRepository;

    public GroupService(ILogger<UserService> logger,
        IBaseRepository<Group> groupRepository)
    {
        _logger = logger;
        _groupRepository = groupRepository;
    }

    public async Task<BaseResponse<IEnumerable<GroupViewModel>>> GetAllGroups()
    {
        try
        {
            var groups = await _groupRepository.GetAll().ToListAsync();

            var views = groups.Select(group => new GroupViewModel
            {
                Id = group.Id,
                Title = group.Title,
                Users = group.Users.Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    Login = user.Login,
                    Role = user.Role
                }).ToList()
            });

            _logger.LogInformation($"[UserService.GetUsers] получено элементов {groups.Count}");
            return new BaseResponse<IEnumerable<GroupViewModel>>()
            {
                Data = views,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UserService.GetUsers] error: {ex.Message}");
            return new BaseResponse<IEnumerable<GroupViewModel>>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}