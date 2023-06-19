using AoSP.Entities;
using AoSP.Enums;
using AoSP.Helpers;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using AoSP.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class TeacherGradeService : ITeacherGradeService
{
    private readonly IBaseRepository<Group> _groupRepository;
    private readonly IBaseRepository<PersonalSubjectTask> _personalSubjectTask;
    private readonly IBaseRepository<User> _userRepository;

    public TeacherGradeService(IBaseRepository<Group> groupRepository,
        IBaseRepository<User> userRepository,
        IBaseRepository<PersonalSubjectTask> personalSubjectTask)
    {
        _groupRepository = groupRepository;
        _personalSubjectTask = personalSubjectTask;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<TeacherGradeViewModel>> Get(string userName)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == userName);

            var teacher = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Role = user.Role,
                Login = user.Login
            };

            //TODO: Доделать
            var subject = new SubjectViewModel
            {
                Id = user.Subject.Id,
                Title = user.Subject.Title,
                Students = user.Subject.PersonalSubjects
                    .Where(x => x.Subject.Teacher.Id == teacher.Id)
                    .Select(x =>
                        new StudentViewModel
                        {
                            Id = x.User.Id,
                            Name = x.User.Name,
                            Surname = x.User.Surname,
                            Patronymic = x.User.Patronymic,
                            Tasks = x.PersonalSubjectTasks.Select(task => new SubjectTaskViewModel
                            {
                                Id = task.Id,
                                Title = task.SubjectTask.Title,
                                Description = task.SubjectTask.Description,
                                Score = task.Score ?? 0,
                                HasFile = !string.IsNullOrEmpty(task.FileName)
                            }).ToList(),
                        }).ToList()
            };

            var views = new TeacherGradeViewModel
            {
                Teacher = teacher,
                Subject = subject,
            };

            return new BaseResponse<TeacherGradeViewModel>()
            {
                Data = views,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<TeacherGradeViewModel>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> SetScore(string id, int score)
    {
        try
        {
            var task = await _personalSubjectTask.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Description = "Задания не существует",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            task.Score = score;

            await _personalSubjectTask.Update(task);

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>
            {
                Data = false,
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> DownloadFile(string id)
    {
        try
        {
            var task = await _personalSubjectTask.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Description = "Задания не существует",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            Helper.DownloadFileFromByteArray(task.FileName, task.File);

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>
            {
                Data = false,
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}