using ATM.Application.Contracts.AccountMode;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Presentation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountModeService _accountModeService;

    public AccountController(IAccountModeService accountModeService)
    {
        _accountModeService = accountModeService;
    }

    [HttpPost("login/user")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
    {
        LoginResultType result = await Task.Run(() => _accountModeService.LoginUser(request.Id, request.Password)).ConfigureAwait(true);

        return result switch
        {
            LoginResultType.Success => Ok("Пользователь успешно авторизован"),
            LoginResultType.InvalidPinFailure => Unauthorized("Неверный PIN-код"),
            LoginResultType.AccountNotFound => NotFound("Пользователь не найден"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }

    [HttpPost("login/admin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminRequest request)
    {
        LoginResultType result = await Task.Run(() => _accountModeService.LoginAdmin(request.Password)).ConfigureAwait(true);

        return result switch
        {
            LoginResultType.Success => Ok("Администратор успешно авторизован"),
            LoginResultType.InvalidPinFailure => Unauthorized("Неверный пароль"),
            LoginResultType.AccountNotFound => NotFound("Администратор не найден"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }
}

#pragma warning disable SA1402
public class LoginUserRequest
#pragma warning restore SA1402
{
    public long Id { get; set; }

    public long Password { get; set; }
}

#pragma warning disable SA1402
public class LoginAdminRequest
#pragma warning restore SA1402
{
    public long Password { get; set; }
}