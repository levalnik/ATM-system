namespace ATM.Application.Contracts.AdminServices;

public abstract record ChangeAdminPasswordResultType
{
    private ChangeAdminPasswordResultType() { }

    public sealed record Success : ChangeAdminPasswordResultType;

    public sealed record InvalidPassword : ChangeAdminPasswordResultType;

    public sealed record Failed : ChangeAdminPasswordResultType;
}