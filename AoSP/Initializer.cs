using AoSP.Entities;
using AoSP.Repositories.Implementations;
using AoSP.Repositories.Interfaces;
using AoSP.Services.Implementations;
using AoSP.Services.Interfaces;

namespace AoSP;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Group>, GroupRepository>();
        services.AddScoped<IBaseRepository<User>, UserRepository>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGroupService, GroupService>();
    }
}