using ATM.Application.Contracts.AccountMode;
using Spectre.Console;
using System.Globalization;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.LoginScenario;

public class AdminLoginScenario : IScenario
{
    private readonly IAccountModeService _accountModeService;

    public AdminLoginScenario(IAccountModeService accountModeService)
    {
        _accountModeService = accountModeService;
    }

    public string Name => "Admin Login";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter your pin: ");
        LoginResultType result = _accountModeService.LoginAdmin(Convert.ToInt64(pin, new CultureInfo(1)));

        string message = result switch
        {
            LoginResultType.Success => "Successfully logged in.",
            LoginResultType.InvalidPinFailure => "Invalid pin",
            LoginResultType.AccountNotFound => "Account not found",
            _ => throw new InvalidOperationException($"Unknown result type: {result}"),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue . . . ");
    }
}