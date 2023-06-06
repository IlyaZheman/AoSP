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
    private readonly IBaseRepository<Group> _groupRepository;

    public GradeService(IBaseRepository<Group> groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<BaseResponse<GradeViewModel>> Get(int selectedGroupId)
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

            return new BaseResponse<GradeViewModel>()
            {
                Data = views,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GradeViewModel>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<Group>> Create(GroupViewModel model)
    {
        try
        {
            var group = await _groupRepository.GetAll().FirstOrDefaultAsync(x => x.Title == model.Title);
            if (group != null)
            {
                return new BaseResponse<Group>()
                {
                    Description = "Группа с таким названием уже есть",
                    StatusCode = StatusCode.GroupAlreadyExist
                };
            }

            group = new Group
            {
                Title = model.Title,
            };

            await _groupRepository.Create(group);

            return new BaseResponse<Group>()
            {
                Data = group,
                Description = "Группа добавлена",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Group>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}