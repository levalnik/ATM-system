using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.MoneyValueObject;
using ATM.Application.Models.Operations;

namespace ATM.Application.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly CurrentUserService _currentUserService;
    private readonly IOperationService _operationService;

    public UserService(
        IUserRepository userRepository,
        CurrentUserService currentUserService,
        IOperationService operationService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _operationService = operationService;
    }

    public OperationResultType Balance()
    {
        if (_currentUserService.User == null)
        {
            return new OperationResultType.Failure();
        }

        Task<Money?> money = _userRepository.Balance(_currentUserService.User.Id);

        return money.Result == null ? new OperationResultType.Failure() : new OperationResultType.Success(money.Result);
    }

    public OperationResultType WithdrawMoney(long value)
    {
        if (_currentUserService.User == null)
        {
            return new OperationResultType.Failure();
        }

        OperationResultType result = _userRepository.WithdrawMoney(_currentUserService.User.Id, new Money(value)).Result;

        if (result is OperationResultType.Success success)
        {
            _operationService.AddOperation(_currentUserService.User.Id, OperationType.WithdrawMoney, value, success.Money.Value);
        }

        return result;
    }

    public OperationResultType TopUpAccount(long value)
    {
        if (_currentUserService.User == null)
        {
            return new OperationResultType.Failure();
        }

        Task<Money> money = _userRepository.TopUpAccount(_currentUserService.User.Id, new Money(value));

        _operationService.AddOperation(_currentUserService.User.Id, OperationType.TopUpAccount, value, money.Result.Value);

        return new OperationResultType.Success(money.Result);
    }
}