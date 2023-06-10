using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly IGradeService _gradeService;

    public AdminController(IUserService userService,
        IGradeService gradeService)
    {
        _userService = userService;
        _gradeService = gradeService;
    }

    [HttpGet]
    public async Task<IActionResult> Grade()
    {
        var response = await _gradeService.Get();
        if (response.StatusCode == Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Grade(GradeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _gradeService.Get(model.SelectedGroupId);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return View(response.Data);
            }

            ModelState.AddModelError("", response.Description);
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateGroup()
    {
        return View(new GroupViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(GroupViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _gradeService.Create(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var response = await _userService.Get(id);

        if (response.StatusCode == Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Edit(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View(model);
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Delete(id);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Grade", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"User ({id}) not deleted");
    }

    public async Task<IActionResult> AddStudent()
    {
        return PartialView("_Student", new UserViewModel());
    }

    public async Task<IActionResult> AddSubject()
    {
        return PartialView("_Subject", new SubjectViewModel());
    }
}