using ATM.Presentation.Console.Scenarios;
using ATM.Presentation.Console.Scenarios.AdminScenarios.AddNewUserScenario;
using ATM.Presentation.Console.Scenarios.AdminScenarios.ChangeAdminPasswordScenario;
using ATM.Presentation.Console.Scenarios.AdminScenarios.ExitScenario;
using ATM.Presentation.Console.Scenarios.AdminScenarios.LoginScenario;
using ATM.Presentation.Console.Scenarios.OperationScenario.GetOperationHistoryScenario;
using ATM.Presentation.Console.Scenarios.UserScenarios.ExitScenario;
using ATM.Presentation.Console.Scenarios.UserScenarios.LoginScenario;
using ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.BalanceScenario;
using ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.TopUpScenario;
using ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.WithdrawScenario;
using Microsoft.Extensions.DependencyInjection;

namespace ATM.Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<IScenarioProvider, UserLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminAddNewUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminExitScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserExitScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminChangeAdminPasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserWithdrawScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserTopUpScenarioProvider>();
        collection.AddScoped<IScenarioProvider, OperationGetOperationHistoryScenarioProvider>();
        collection.AddScoped<ScenarioRunner>();

        return collection;
    }
}