using AoSP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Detail()
        {
            var response = await _userService.GetByUserName(User.Identity.Name);
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return View(response.Data);
            }

            throw new Exception($"Profile {User.Identity.Name} not found");
        }
    }
}