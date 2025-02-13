using ATM.Application.Contracts.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Presentation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangeAdminPasswordRequest request)
    {
        ChangeAdminPasswordResultType result = await Task.Run(() => _adminService.ChangeAdminPassword(request.OldPassword, request.NewPassword)).ConfigureAwait(true);

        return result switch
        {
            ChangeAdminPasswordResultType.Success => Ok("Пароль администратора успешно изменён"),
            ChangeAdminPasswordResultType.InvalidPassword => Unauthorized("Неверный старый пароль"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> AddNewUser([FromBody] AddNewUserRequest request)
    {
        AddNewUserResultType result = await Task.Run(() => _adminService.AddNewUser(request.Pin)).ConfigureAwait(true);

        return result switch
        {
            AddNewUserResultType.Success success => Ok(new { success.Pin }),
            AddNewUserResultType.Failure => BadRequest("Не удалось создать пользователя"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }
}

#pragma warning disable SA1402
public class ChangeAdminPasswordRequest
#pragma warning restore SA1402
{
    public long OldPassword { get; set; }

    public long NewPassword { get; set; }
}

#pragma warning disable SA1402
public class AddNewUserRequest
#pragma warning restore SA1402
{
    public long Pin { get; set; }
}