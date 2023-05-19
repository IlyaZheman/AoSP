using AoSP.Services.Interfaces;
using AoSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AoSP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Login");
            if (ModelState.IsValid)
            {
                var response = await _profileService.Save(model);
                if (response.StatusCode == Enums.StatusCode.Ok)
                {
                    return Json(new { description = response.Description });
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Detail()
        {
            if (User.Identity == null)
                throw new Exception("User identity = null");
            var userName = User.Identity.Name;
            if (userName == null)
                throw new Exception("User identity name = null");
            var response = await _profileService.GetProfile(userName);
            if (response == null)
                throw new Exception($"Profile {userName} not found");

            return response.StatusCode == Enums.StatusCode.Ok ? View(response.Data) : View();
        }
    }
}