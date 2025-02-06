using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Models.OperationEntity;
using ATM.Application.Models.Operations;
using ATM.Application.UserServices;

namespace ATM.Application.OperationServices;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepository;
    private readonly CurrentUserService _currentUserService;

    public OperationService(IOperationRepository operationRepository, CurrentUserService currentUserService)
    {
        _operationRepository = operationRepository;
        _currentUserService = currentUserService;
    }

    public IEnumerable<Operation> GetOperationHistory()
    {
        if (_currentUserService.User is null)
        {
            throw new ArgumentNullException(nameof(_currentUserService.User));
        }

        return _operationRepository.GetOperationHistory(_currentUserService.User.Id).Result;
    }

    public CreateOperationHistoryResultType AddOperation(long id, OperationType operationType, long value, long balance)
    {
        return _operationRepository.AddOperation(id, operationType, value, balance).Result;
    }
}