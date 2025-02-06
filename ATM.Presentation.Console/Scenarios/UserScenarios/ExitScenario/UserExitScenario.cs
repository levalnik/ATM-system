using ATM.Application.AccountModeServices;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.ExitScenario;

public class UserExitScenario : IScenario
{
    private readonly CurrentAccountModeService _service;

    public UserExitScenario(CurrentAccountModeService service)
    {
        _service = service;
    }

    public string Name => "Exit";

    public void Run()
    {
        _service.CurrentUserMode = null;
    }
}