using System.Security.Claims;
using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.Login(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                var hasPasswordResponse = await _accountService.HasPassword(model);

                if (hasPasswordResponse.StatusCode == Enums.StatusCode.Ok)
                {
                    Console.WriteLine($"Ok");
                    if (hasPasswordResponse.Data)
                    {
                        var passwordModel = new PasswordViewModel { Login = model.Login };
                        return RedirectToAction("Password", "Account", passwordModel);
                    }
                    else
                    {
                        var createPasswordModel = new CreatePasswordViewModel { Login = model.Login };
                        return RedirectToAction("CreatePassword", "Account", createPasswordModel);
                    }
                }

                ModelState.AddModelError("", hasPasswordResponse.Description);
            }

            ModelState.AddModelError("", response.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Password(PasswordViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    [ActionName("Password")]
    public async Task<IActionResult> PasswordPost(PasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.Password(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View();
    }

    [HttpGet]
    public IActionResult CreatePassword(CreatePasswordViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    [ActionName("CreatePassword")]
    public async Task<IActionResult> CreatePasswordPost(CreatePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.CreatePassword(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.ChangePassword(model);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return Json(new { description = response.Description });
            }
        }

        var modelError = ModelState.Values.SelectMany(v => v.Errors);

        return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
    }
}