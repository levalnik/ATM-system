using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.MoneyValueObject;
using ATM.Application.Models.UserValueObject;

namespace ATM.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> FindAccountById(long id);

    Task<Money?> Balance(long id);

    Task<OperationResultType> WithdrawMoney(long id, Money updateMoney);

    Task<Money> TopUpAccount(long id, Money updateMoney);
}