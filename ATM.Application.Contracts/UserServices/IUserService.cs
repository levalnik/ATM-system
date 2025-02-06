namespace ATM.Application.Contracts.UserServices;

public interface IUserService
{
    OperationResultType Balance();

    OperationResultType WithdrawMoney(long value);

    OperationResultType TopUpAccount(long value);
}