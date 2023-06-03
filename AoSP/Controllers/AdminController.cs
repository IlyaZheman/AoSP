using AoSP.Extensions;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    public AdminController(ILogger<HomeController> logger,
        IUserService userService,
        IGroupService groupService)
    {
        _logger = logger;
        _userService = userService;
        _groupService = groupService;
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

    public async Task<IActionResult> Groups()
    {
        var response = await _groupService.GetAllGroups();
        return response.StatusCode == Enums.StatusCode.Ok ? View(response.Data) : View();
    }
}