using ATM.Application.AccountModeServices;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.ExitScenario;

public class AdminExitScenario : IScenario
{
    private readonly CurrentAccountModeService _currentAccountModeService;

    public AdminExitScenario(CurrentAccountModeService currentAccountModeService)
    {
        _currentAccountModeService = currentAccountModeService;
    }

    public string Name => "Exit";

    public void Run()
    {
        _currentAccountModeService.CurrentUserMode = null;
    }
}