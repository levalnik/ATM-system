using ATM.Application.Contracts.AdminServices;
using Spectre.Console;
using System.Globalization;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.AddNewUserScenario;

public class AdminAddNewUserScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminAddNewUserScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Add New User";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter the pin: ");
        AddNewUserResultType result = _adminService.AddNewUser(Convert.ToInt64(pin, new CultureInfo(1)));

        string message = result switch
        {
            AddNewUserResultType.Success success => "Registration successful. AccountNumber:" +
                                                    $" {success.UserId}, Pin: {success.Pin}",
            AddNewUserResultType.Failure => "This user is already exists!",
            _ => throw new InvalidOperationException(),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press any key to continue . . . ");
    }
}