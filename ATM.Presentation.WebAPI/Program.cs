using ATM.Application.AccountModeServices;
using ATM.Application.AdminServices;
using ATM.Application.Contracts.AccountMode;
using ATM.Application.Contracts.AdminServices;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.OperationServices;
using ATM.Application.UserServices;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов приложения
builder.Services.AddScoped<IAccountModeService, AccountModeService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddControllers();
WebApplication app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();