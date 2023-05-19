using AoSP.Entities;
using AoSP.Response;
using AoSP.ViewModels;

namespace AoSP.Services.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<ProfileViewModel>> GetProfile(string login);

        Task<IBaseResponse<Profile>> Save(ProfileViewModel model);
    }
}