using ATM.Application.AccountModeServices;
using ATM.Application.AdminServices;
using ATM.Application.Contracts.AccountMode;
using ATM.Application.Contracts.AdminServices;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.OperationServices;
using ATM.Application.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace ATM.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAdminService, AdminService>();
        collection.AddScoped<IAccountModeService, AccountModeService>();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IOperationService, OperationService>();
        collection.AddScoped<CurrentUserService>();
        collection.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUserService>());
        collection.AddScoped<CurrentAdminService>();
        collection.AddScoped<ICurrentAdminService>(
            p => p.GetRequiredService<CurrentAdminService>());
        collection.AddScoped<CurrentAccountModeService>();
        collection.AddScoped<ICurrentAccountModeService>(
            p => p.GetRequiredService<CurrentAccountModeService>());
        return collection;
    }
}