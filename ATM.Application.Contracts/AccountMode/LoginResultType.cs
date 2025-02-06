namespace ATM.Application.Contracts.AccountMode;

public abstract record LoginResultType
{
    private LoginResultType() { }

    public sealed record Success : LoginResultType;

    public sealed record AccountNotFound : LoginResultType;

    public sealed record InvalidPinFailure : LoginResultType;
}