using ATM.Application.Contracts.AccountMode;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.LoginScenario;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IAccountModeService _accountModeService;
    private readonly ICurrentAccountModeService _currentAccountModeService;

    public AdminLoginScenarioProvider(
        IAccountModeService accountModeService,
        ICurrentAccountModeService currentAccountMode)
    {
        _accountModeService = accountModeService;
        _currentAccountModeService = currentAccountMode;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminLoginScenario(_accountModeService);
        return true;
    }
}