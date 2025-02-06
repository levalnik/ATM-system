using ATM.Application.AccountModeServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.WithdrawScenario;

public class UserWithdrawScenarioProvider : IScenarioProvider
{
    private readonly CurrentAccountModeService _currentAccountModeService;
    private readonly IUserService _userService;

    public UserWithdrawScenarioProvider(IUserService userService, CurrentAccountModeService currentAccountModeService)
    {
        _userService = userService;
        _currentAccountModeService = currentAccountModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode == AccountType.User)
        {
            scenario = new UserWithdrawScenario(_userService);
            return true;
        }

        scenario = null;
        return false;
    }
}