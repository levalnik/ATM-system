using ATM.Application.AccountModeServices;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.OperationScenario.GetOperationHistoryScenario;

public class OperationGetOperationHistoryScenarioProvider : IScenarioProvider
{
    private readonly CurrentAccountModeService _currentAccountModeService;
    private readonly IOperationService _operationService;

    public OperationGetOperationHistoryScenarioProvider(
        CurrentAccountModeService currentAccountModeService,
        IOperationService operationService)
    {
        _currentAccountModeService = currentAccountModeService;
        _operationService = operationService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountModeService.CurrentUserMode == AccountType.User)
        {
            scenario = new OperationGetOperationHistoryScenario(_operationService);
            return true;
        }

        scenario = null;
        return false;
    }
}