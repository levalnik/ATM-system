using ATM.Application.Contracts.UserServices;
using Spectre.Console;
using System.Globalization;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.TopUpScenario;

public class UserTopUpScenario : IScenario
{
    private readonly IUserService _userService;

    public UserTopUpScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Top up account";

    public void Run()
    {
        string value = AnsiConsole.Ask<string>("Enter the amount you would like the top up account: ");
        OperationResultType result = _userService.TopUpAccount(Convert.ToInt64(value, new CultureInfo(1)));
        string message = result switch
        {
            OperationResultType.Success success => "Balance:" + $" {success.Money.Value}",
            OperationResultType.Failure => "Invalid input",
            _ => throw new InvalidOperationException(),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue . . . ");
    }
}