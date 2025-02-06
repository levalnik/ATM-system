using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}