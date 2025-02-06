namespace ATM.Application.Contracts.OperationServices;

public abstract record CreateOperationHistoryResultType
{
    private CreateOperationHistoryResultType() { }

    public sealed record Success : CreateOperationHistoryResultType;

    public sealed record Failure : CreateOperationHistoryResultType;
}