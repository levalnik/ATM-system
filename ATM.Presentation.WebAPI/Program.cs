using ATM.Application.AccountModeServices;
using ATM.Application.AdminServices;
using ATM.Application.Contracts.AccountMode;
using ATM.Application.Contracts.AdminServices;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.OperationServices;
using ATM.Application.UserServices;
using ATM.Application.Abstractions.Repositories;
using ATM.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using ATM.Application.Extensions;
using ATM.Infrastructure.DataAccess.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddInfrastructureDataAccess(options =>
{
    options.Host = "localhost";
    options.Port = 6432;
    options.Database = "postgres";
    options.Username = builder.Configuration["Database:Username"] ?? "postgres";
    options.Password = builder.Configuration["Database:Password"] ?? "postgres";
    options.SslMode = "Prefer";
});

builder.Services.AddScoped<CurrentAccountModeService>();

builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<IAccountModeService, AccountModeService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    scope.UseInfrastructureDataAccess();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();