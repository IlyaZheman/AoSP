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
            ModelState.Remove("UserName");
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
            var userName = User.Identity.Name;
            var response = await _profileService.GetProfile(userName);
            Console.WriteLine($"Вход выполнен юзером: {User.Identity.Name}");
            
            if (response.StatusCode == Enums.StatusCode.Ok)
            {
                return View(response.Data);   
            }
            return View();
        }
    }
}