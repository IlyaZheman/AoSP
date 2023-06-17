using AoSP.Entities;
using AoSP.Enums;
using AoSP.Helpers;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using AoSP.ViewModels.Student;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AoSP.Services.Implementations;

public class StudentGradeService : IStudentGradeService
{
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly IBaseRepository<Group> _groupRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<PersonalSubjectTask> _personalSubjectTask;

    public StudentGradeService(IHostingEnvironment hostingEnvironment,
        IBaseRepository<Group> groupRepository,
        IBaseRepository<User> userRepository,
        IBaseRepository<PersonalSubjectTask> personalSubjectTask)
    {
        _hostingEnvironment = hostingEnvironment;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _personalSubjectTask = personalSubjectTask;
    }

    public async Task<BaseResponse<StudentGradeViewModel>> Get(string userName)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == userName);

            var student = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Role = user.Role,
                Login = user.Login
            };

            var subjects = user.PersonalSubjects.Select(x => new PersonalSubjectViewModel
            {
                Id = x.Id,
                Title = x.Subject.Title,
                Teacher = new UserViewModel
                {
                    Id = x.Subject.Teacher.Id,
                    Name = x.Subject.Teacher.Name,
                    Surname = x.Subject.Teacher.Surname,
                    Patronymic = x.Subject.Teacher.Patronymic,
                    Login = x.Subject.Teacher.Login,
                    Role = x.Subject.Teacher.Role,
                },
                PersonalSubjectTasks = x.PersonalSubjectTasks.Select(s => new PersonalSubjectTaskViewModel
                {
                    Id = s.Id,
                    TaskTitle = s.SubjectTask.Title,
                    Score = s.Score,
                    //TODO:: Дописать
                    // File = new FormFile(new MemoryStream(s.File), 0, s.File.Length, "name", "fileName"),
                }).ToList(),
            }).ToList();
            
            var views = new StudentGradeViewModel
            {
                Student = student,
                PersonalSubjects = subjects,
            };

            return new BaseResponse<StudentGradeViewModel>()
            {
                Data = views,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<StudentGradeViewModel>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    // https://stackoverflow.com/questions/35379309/how-to-upload-files-in-asp-net-core
    public async Task<BaseResponse<bool>> UploadFile(PersonalSubjectTaskViewModel model)
    {
        try
        {
            var task = await _personalSubjectTask.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (task == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = "Задания не существует",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            task.File = Helper.GetByteArrayFromFile(model.File);

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

            Helper.DownloadFileFromByteArray(task.File);

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