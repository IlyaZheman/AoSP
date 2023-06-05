using AoSP.Extensions;
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

    public async Task<IActionResult> Users()
    {
        var response = await _userService.GetAllUsers();
        return response.StatusCode == Enums.StatusCode.Ok ? View(response.Data) : View();
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Create(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Users", "Admin");
            }

            return BadRequest(new { errorMessage = response.Description });
        }

        var errorMessage = ModelState.Values
            .SelectMany(v => v.Errors.Select(x => x.ErrorMessage))
            .ToList()
            .Join();
        return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
    {
        var response = await _userService.GetUser(id);

        if (response.StatusCode == Enums.StatusCode.Ok)
        {
            return View(response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(int id, UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Edit(id, model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Users", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View(model);
    }

    public async Task<IActionResult> DeleteUser(int id)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Users", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"User ({id}) not deleted");
    }

    [HttpGet]
    public async Task<IActionResult> Grade()
    {
        var response = await _gradeService.GetGrade(1);
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
            var response = await _gradeService.GetGrade(model.SelectedGroupId);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return View(response.Data);
            }

            ModelState.AddModelError("", response.Description);
        }

        return View();
    }
}