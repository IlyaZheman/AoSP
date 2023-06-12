using AoSP.Entities;
using AoSP.Enums;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels.Student;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class StudentGradeService : IStudentGradeService
{
    private readonly IBaseRepository<Group> _groupRepository;

    public StudentGradeService(IBaseRepository<Group> groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<BaseResponse<StudentGradeViewModel>> Get(string userName)
    {
        try
        {
            var groups = await _groupRepository.GetAll().ToListAsync();

            var views = new StudentGradeViewModel
            {
                
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
}