using ATM.Application.Contracts.AccountMode;
using ATM.Application.Contracts.AdminServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.AddNewUserScenario;

public class AdminAddNewUserScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAccountModeService _currentAccountModeService;

    public AdminAddNewUserScenarioProvider(IAdminService adminService, ICurrentAccountModeService currentAccountModeService)
    {
        _adminService = adminService;
        _currentAccountModeService = currentAccountModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode == AccountType.Admin)
        {
            scenario = new AdminAddNewUserScenario(_adminService);
            return true;
        }

        scenario = null;
        return false;
    }
}