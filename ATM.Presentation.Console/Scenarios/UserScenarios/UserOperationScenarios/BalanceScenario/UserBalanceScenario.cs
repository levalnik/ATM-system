using ATM.Application.Contracts.UserServices;
using Spectre.Console;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.BalanceScenario;

public class UserBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public UserBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Balance";

    public void Run()
    {
        OperationResultType result = _userService.Balance();

        string message = result switch
        {
            OperationResultType.Success success => "Balance:" + $" {success.Money.Value}",
            _ => throw new InvalidOperationException(),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue...");
    }
}