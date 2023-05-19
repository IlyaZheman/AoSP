using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;

    public AdminController(ILogger<HomeController> logger,
                           IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _userService.GetAllUsers();
        return response.StatusCode == Enums.StatusCode.Ok ? View(response.Data) : View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
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
    public async Task<IActionResult> Edit(int id, UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Edit(id, model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", response.Description);
        }

        throw new Exception($"User ({id}) not deleted");
    }
}