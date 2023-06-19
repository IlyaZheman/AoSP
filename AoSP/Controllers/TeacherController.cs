using AoSP.Services.Interfaces;
using AoSP.ViewModels.Student;
using AoSP.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class TeacherController : Controller
{
    private readonly IUserService _userService;
    private readonly ITeacherGradeService _teacherGradeService;

    public TeacherController(IUserService userService,
        ITeacherGradeService teacherGradeService)
    {
        _userService = userService;
        _teacherGradeService = teacherGradeService;
    }

    public async Task<IActionResult> Grade()
    {
        var response = await _teacherGradeService.Get(User.Identity.Name);
        if (response.StatusCode == Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SetScore(SubjectTaskViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _teacherGradeService.SetScore(model.Id, model.Score);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"{model.Id} not set");
    }

    [HttpPost]
    public async Task<IActionResult> DownloadFile(SubjectTaskViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _teacherGradeService.DownloadFile(model.Id);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"{model.Id} not downloaded");
    }
}