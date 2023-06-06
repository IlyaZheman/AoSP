﻿using AoSP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class TeacherController : Controller
{
    private readonly IUserService _userService;
    private readonly IGradeService _gradeService;

    public TeacherController(IUserService userService,
        IGradeService gradeService)
    {
        _userService = userService;
        _gradeService = gradeService;
    }
}