using ATM.Application.AccountModeServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.ExitScenario;

public class AdminExitScenarioProvider : IScenarioProvider
{
    private readonly CurrentAccountModeService _currentAccountModeService;

    public AdminExitScenarioProvider(CurrentAccountModeService currentAccountModeService)
    {
        _currentAccountModeService = currentAccountModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode == AccountType.Admin)
        {
            scenario = new AdminExitScenario(_currentAccountModeService);
            return true;
        }

        scenario = null;
        return false;
    }
}