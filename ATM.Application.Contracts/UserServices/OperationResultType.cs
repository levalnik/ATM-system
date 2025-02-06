using ATM.Application.Models.MoneyValueObject;

namespace ATM.Application.Contracts.UserServices;

public abstract record OperationResultType
{
    private OperationResultType() { }

    public sealed record Success(Money Money) : OperationResultType;

    public sealed record Failure : OperationResultType;
}