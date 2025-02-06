using ATM.Application.AccountModeServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.ExitScenario;

public class UserExitScenarioProvider : IScenarioProvider
{
    private readonly CurrentAccountModeService _service;

    public UserExitScenarioProvider(CurrentAccountModeService service)
    {
        _service = service;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_service.CurrentUserMode == AccountType.User)
        {
            scenario = new UserExitScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}