using ATM.Application.Contracts.UserServices;
using Spectre.Console;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.WithdrawScenario;

public class UserWithdrawScenario : IScenario
{
    private readonly IUserService _userService;

    public UserWithdrawScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Withdraw money";

    public void Run()
    {
        string value = AnsiConsole.Ask<string>("Enter the amount you want to withdraw");
        OperationResultType result = _userService.WithdrawMoney(Convert.ToInt64(value));
        string message = result switch
        {
            OperationResultType.Success success => "Balance:" + $"{success.Money.Value}",
            OperationResultType.Failure => "Invalid input",
            _ => throw new InvalidOperationException(),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue...");
    }
}