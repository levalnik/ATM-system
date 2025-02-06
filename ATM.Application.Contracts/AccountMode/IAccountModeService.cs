namespace ATM.Application.Contracts.AccountMode;

public interface IAccountModeService
{
    LoginResultType LoginAdmin(long password);

    LoginResultType LoginUser(long id, long password);
}