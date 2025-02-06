using ATM.Application.AccountModeServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.TopUpScenario;

public class UserTopUpScenarioProvider : IScenarioProvider
{
    private readonly CurrentAccountModeService _currentAccountModeService;
    private readonly IUserService _userService;

    public UserTopUpScenarioProvider(IUserService userService, CurrentAccountModeService currentAccountModeService)
    {
        _userService = userService;
        _currentAccountModeService = currentAccountModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode == AccountType.User)
        {
            scenario = new UserTopUpScenario(_userService);
            return true;
        }

        scenario = null;
        return false;
    }
}