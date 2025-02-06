using ATM.Application.Contracts.AccountMode;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.LoginScenario;

public class UserLoginScenarioProvider : IScenarioProvider
{
    private readonly IAccountModeService _accountModeService;
    private readonly ICurrentAccountModeService _currentAccountModeService;

    public UserLoginScenarioProvider(
        IAccountModeService accountModeService,
        ICurrentAccountModeService currentAccountModeService)
    {
        _accountModeService = accountModeService;
        _currentAccountModeService = currentAccountModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new UserLoginScenario(_accountModeService);
        return true;
    }
}