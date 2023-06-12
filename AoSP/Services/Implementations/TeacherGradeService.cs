using AoSP.Entities;
using AoSP.Enums;
using AoSP.Repositories.Interfaces;
using AoSP.Response;
using AoSP.Services.Interfaces;
using AoSP.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Services.Implementations;

public class TeacherGradeService : ITeacherGradeService
{
    private readonly IBaseRepository<Group> _groupRepository;

    public TeacherGradeService(IBaseRepository<Group> groupRepository)
    {
        _groupRepository = groupRepository;
    }
    
    public async Task<BaseResponse<TeacherGradeViewModel>> Get(string userName)
    {
        try
        {
            var groups = await _groupRepository.GetAll().ToListAsync();

            var views = new TeacherGradeViewModel
            {
                
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
}