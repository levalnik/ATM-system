using ATM.Application.Contracts.OperationServices;
using ATM.Application.Models.OperationEntity;
using ATM.Application.Models.Operations;

namespace ATM.Application.Abstractions.Repositories;

public interface IOperationRepository
{
    Task<IEnumerable<Operation>> GetOperationHistory(long id);

    Task<CreateOperationHistoryResultType> AddOperation(
        long id,
        OperationType operationType,
        long value,
        long balance);
}