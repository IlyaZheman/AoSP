using AoSP.Services.Interfaces;
using AoSP.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class StudentController : Controller
{
    private readonly IUserService _userService;
    private readonly IStudentGradeService _studentGradeService;

    public StudentController(IUserService userService,
        IStudentGradeService studentGradeService)
    {
        _userService = userService;
        _studentGradeService = studentGradeService;
    }

    public async Task<IActionResult> Grade()
    {
        var response = await _studentGradeService.Get(User.Identity.Name);
        if (response.StatusCode == Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(PersonalSubjectTaskViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _studentGradeService.UploadFile(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"{model.File.Name} not saved");
    }
    
    [HttpPost]
    public async Task<IActionResult> DownloadFile(PersonalSubjectTaskViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _studentGradeService.DownloadFile(model.Id);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"{model.File.Name} not saved");
    }
}