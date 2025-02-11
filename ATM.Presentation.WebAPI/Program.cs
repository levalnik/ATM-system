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

// Добавляем сервисы приложения
builder.Services.AddApplication();

// Добавляем инфраструктуру
builder.Services.AddInfrastructureDataAccess(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Uri uri = new Uri(connectionString ?? throw new InvalidOperationException("Connection string is not found"));
    options.Host = uri.Host;
    options.Port = uri.Port;
    options.Database = uri.Segments[1];
    options.Username = builder.Configuration["Database:Username"] ?? "postgres";
    options.Password = builder.Configuration["Database:Password"] ?? "postgres";
    options.SslMode = "Prefer";
});

// Регистрация вспомогательных сервисов
builder.Services.AddScoped<CurrentAccountModeService>();

// Регистрация репозиториев
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

// Регистрация сервисов приложения
builder.Services.AddScoped<IAccountModeService, AccountModeService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddControllers();

// Добавление Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Применяем миграции
using (IServiceScope scope = app.Services.CreateScope())
{
    scope.UseInfrastructureDataAccess();
}

// Конфигурация HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();