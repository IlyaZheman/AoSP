using AoSP.Entities;
using AoSP.Enums;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class GradeService : IGradeService
{
    private readonly ILogger<UserService> _logger;
    private readonly IBaseRepository<Group> _groupRepository;

    public GradeService(ILogger<UserService> logger,
        IBaseRepository<Group> groupRepository)
    {
        _logger = logger;
        _groupRepository = groupRepository;
    }

    public async Task<BaseResponse<GradeViewModel>> GetGrade(int selectedGroupId)
    {
        try
        {
            var groups = await _groupRepository.GetAll().ToListAsync();

            var views = new GradeViewModel
            {
                SelectedGroupId = selectedGroupId,
                Groups = groups.Select(group => new GroupViewModel
                {
                    Id = group.Id,
                    Title = group.Title,
                    Subjects = group.Subjects.Select(subject => new SubjectViewModel
                    {
                        Id = subject.Id,
                        Title = subject.Title,
                        SubjectTasks = subject.SubjectTasks.Select(subjectTask => new SubjectTaskViewModel
                        {
                            Id = subjectTask.Id,
                            Description = subjectTask.Description
                        }).ToList()
                    }).ToList(),
                    Users = group.Students.Select(user => new UserViewModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Patronymic = user.Patronymic,
                        Login = user.Login,
                        Role = user.Role
                    }).ToList()
                }).ToList()
            };

            _logger.LogInformation($"[UserService.GetUsers] получено элементов {groups.Count}");
            return new BaseResponse<GradeViewModel>()
            {
                Data = views,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[UserService.GetUsers] error: {ex.Message}");
            return new BaseResponse<GradeViewModel>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}