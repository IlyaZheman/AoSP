using AoSP.Entities;
using AoSP.Enums;
using AoSP.Helpers;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using AoSP.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class GradeService : IGradeService
{
    private readonly IBaseRepository<Group> _groupRepository;

    public GradeService(IBaseRepository<Group> groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<BaseResponse<GradeViewModel>> Get(string? selectedGroupId = null)
    {
        try
        {
            var groups = await _groupRepository.GetAll().ToListAsync();

            if (groups == null || groups.Count <= 0)
            {
                return new BaseResponse<GradeViewModel>
                {
                    Data = new GradeViewModel
                    {
                        SelectedGroupId = "",
                        Groups = new List<GroupViewModel>()
                    },
                    StatusCode = StatusCode.Ok
                };
            }

            var views = new GradeViewModel
            {
                SelectedGroupId = selectedGroupId ?? groups.First().Id,
                Groups = groups.Select(group => new GroupViewModel
                {
                    GroupId = group.Id,
                    GroupTitle = group.Title,
                    Subjects = group.Subjects.Select(subject => new SubjectViewModel
                    {
                        Id = subject.Id,
                        Title = subject.Title,
                        Teacher = new UserViewModel
                        {
                            Id = subject.Teacher.Id,
                            Name = subject.Teacher.Name,
                            Surname = subject.Teacher.Surname,
                            Patronymic = subject.Teacher.Patronymic,
                            Login = subject.Teacher.Login,
                            Role = subject.Teacher.Role,
                        },
                        SubjectTasks = subject.SubjectTasks.Select(subjectTask => new SubjectTaskViewModel
                        {
                            Id = subjectTask.Id,
                            Description = subjectTask.Description,
                        }).ToList()
                    }).ToList(),
                    Students = group.Students.Select(user => new UserViewModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Patronymic = user.Patronymic,
                        Login = user.Login,
                        Role = user.Role,
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
            var group = await _groupRepository.GetAll().FirstOrDefaultAsync(x => x.Title == model.GroupTitle);
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
                Id = Helper.GenerateId(),
                Title = model.GroupTitle,
                Students = model.Students.Select(x => new User
                {
                    Id = Helper.GenerateId(),
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    Role = Role.Student
                }).ToList(),
                Subjects = model.Subjects.Select(x => new Subject
                {
                    Title = x.Title,
                    Teacher = new User
                    {
                        Id = Helper.GenerateId(),
                        Name = x.Teacher.Name,
                        Surname = x.Teacher.Surname,
                        Patronymic = x.Teacher.Patronymic,
                        Role = Role.Teacher
                    }
                }).ToList(),
            };

            foreach (var student in group.Students)
            {
                student.Group = group;
                student.GroupId = group.Id;
            }

            foreach (var subject in group.Subjects)
            {
                subject.Group = group;
                subject.GroupId = group.Id;
            }

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