using ATM.Application.Contracts.OperationServices;
using ATM.Application.Models.OperationEntity;
using Spectre.Console;
using System.Globalization;

namespace ATM.Presentation.Console.Scenarios.OperationScenario.GetOperationHistoryScenario;

public class OperationGetOperationHistoryScenario : IScenario
{
    private readonly IOperationService _operationService;

    public OperationGetOperationHistoryScenario(IOperationService operationService)
    {
        _operationService = operationService;
    }

    public string Name => "Get Operation History";

    public void Run()
    {
        IEnumerable<Operation> result = _operationService.GetOperationHistory();
        var table = new Table();
        table.AddColumn("Operation");
        table.AddColumn(new TableColumn("Value").Centered());
        table.AddColumn(new TableColumn("Balance").Centered());
        foreach (Operation row in result)
        {
            table.AddRow(
                row.OperationType.ToString(),
                row.Value.ToString(CultureInfo.InvariantCulture),
                row.Balance.ToString(CultureInfo.InvariantCulture));
            AnsiConsole.WriteLine();
        }

        AnsiConsole.Write(table);
        AnsiConsole.Ask<string>("Press any key to continue...");
    }
}