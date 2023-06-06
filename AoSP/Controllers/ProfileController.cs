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
            if (User.Identity == null)
                throw new Exception("User identity = null");
            var userName = User.Identity.Name;
            if (userName == null)
                throw new Exception("User identity name = null");
            var response = await _userService.Get(userName);
            if (response == null)
                throw new Exception($"Profile {userName} not found");

            return response.StatusCode == Enums.StatusCode.Ok ? View(response.Data) : View();
        }
    }
}