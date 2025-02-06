namespace ATM.Application.Contracts.AdminServices;

public abstract record AddNewUserResultType
{
    private AddNewUserResultType() { }

    public sealed record Success(long UserId, long Pin) : AddNewUserResultType;

    public sealed record Failure : AddNewUserResultType;
}