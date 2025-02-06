using ATM.Application.AccountModeServices;
using ATM.Application.Contracts.AdminServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.AdminScenarios.ChangeAdminPasswordScenario;

public class AdminChangeAdminPasswordScenarioProvider : IScenarioProvider
{
    private readonly CurrentAccountModeService _currentAccountModeService;
    private readonly IAdminService _adminService;

    public AdminChangeAdminPasswordScenarioProvider(
        CurrentAccountModeService currentAccountModeService,
        IAdminService adminService)
    {
        _currentAccountModeService = currentAccountModeService;
        _adminService = adminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode == AccountType.Admin)
        {
            scenario = new AdminChangeAdminPasswordScenario(_adminService);
            return true;
        }

        scenario = null;
        return false;
    }
}