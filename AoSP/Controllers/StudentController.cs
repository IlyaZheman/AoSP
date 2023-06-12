using AoSP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class StudentController : Controller
{
    private readonly IUserService _userService;
    private readonly IAdminGradeService _adminGradeService;

    public StudentController(IUserService userService,
        IAdminGradeService adminGradeService)
    {
        _userService = userService;
        _adminGradeService = adminGradeService;
    }
}