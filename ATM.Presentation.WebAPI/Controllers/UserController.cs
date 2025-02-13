using ATM.Application.Contracts.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Presentation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance()
    {
        OperationResultType result = await Task.Run(() => _userService.Balance()).ConfigureAwait(true);

        return result switch
        {
            OperationResultType.Success success => Ok(new { Balance = success.Money.Value }),
            OperationResultType.Failure => Unauthorized("Пользователь не авторизован или ошибка получения баланса"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawMoney([FromBody] WithdrawMoneyRequest request)
    {
        OperationResultType result = await Task.Run(() => _userService.WithdrawMoney(request.Value)).ConfigureAwait(true);

        return result switch
        {
            OperationResultType.Success success => Ok(new { RemainingBalance = success.Money.Value }),
            OperationResultType.Failure => BadRequest("Ошибка снятия денег (недостаточно средств или пользователь не авторизован)"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }

    [HttpPost("top-up")]
    public async Task<IActionResult> TopUpAccount([FromBody] TopUpAccountRequest request)
    {
        OperationResultType result = await Task.Run(() => _userService.TopUpAccount(request.Value)).ConfigureAwait(true);

        return result switch
        {
            OperationResultType.Success success => Ok(new { NewBalance = success.Money.Value }),
            OperationResultType.Failure => BadRequest("Ошибка пополнения (пользователь не авторизован)"),
            _ => StatusCode(500, "Неизвестная ошибка"),
        };
    }
}

#pragma warning disable SA1402
public class WithdrawMoneyRequest
#pragma warning restore SA1402
{
    public long Value { get; set; }
}

#pragma warning disable SA1402
public class TopUpAccountRequest
#pragma warning restore SA1402
{
    public long Value { get; set; }
}