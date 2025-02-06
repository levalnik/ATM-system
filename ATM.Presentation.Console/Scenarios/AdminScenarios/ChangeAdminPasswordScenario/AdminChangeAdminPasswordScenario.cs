using ATM.Application.Contracts.AdminServices;
using Spectre.Console;
using System.Globalization;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.ChangeAdminPasswordScenario;

public class AdminChangeAdminPasswordScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminChangeAdminPasswordScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Change Admin Password";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter your pin: ");
        string newPassword = AnsiConsole.Ask<string>("Enter new password: ");

        ChangeAdminPasswordResultType result =
            _adminService.ChangeAdminPassword(
                Convert.ToInt64(pin, new CultureInfo(1)),
                Convert.ToInt64(newPassword, new CultureInfo(1)));
        string message = result switch
        {
            ChangeAdminPasswordResultType.Success => "Password changed successfully.",
            ChangeAdminPasswordResultType.InvalidPassword => "Invalid password.",
            ChangeAdminPasswordResultType.Failed => "Something went wrong =(.",
            _ => throw new ArgumentOutOfRangeException(),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue . . . ");
    }
}