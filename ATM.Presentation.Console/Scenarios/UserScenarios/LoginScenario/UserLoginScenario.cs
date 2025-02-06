using ATM.Application.Contracts.AccountMode;
using Spectre.Console;
using System.Globalization;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.LoginScenario;

public class UserLoginScenario : IScenario
{
    private readonly IAccountModeService _service;

    public UserLoginScenario(IAccountModeService service)
    {
        _service = service;
    }

    public string Name => "Login";

    public void Run()
    {
        string id = AnsiConsole.Ask<string>("Enter user id: ");
        string pin = AnsiConsole.Ask<string>("Enter PIN-code: ");

        LoginResultType result = _service.LoginUser(
            Convert.ToInt64(id, new CultureInfo(1)),
            Convert.ToInt64(pin, new CultureInfo(1)));

        string message = result switch
        {
            LoginResultType.Success => "Login successful!",
            LoginResultType.InvalidPinFailure => "Invalid PIN-code!",
            LoginResultType.AccountNotFound => "Account not found!",
            _ => throw new NotImplementedException(),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue . . . ");
    }
}