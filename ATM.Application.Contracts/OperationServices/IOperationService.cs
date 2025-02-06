using ATM.Application.Models.OperationEntity;
using ATM.Application.Models.Operations;

namespace ATM.Application.Contracts.OperationServices;

public interface IOperationService
{
    IEnumerable<Operation> GetOperationHistory();

    CreateOperationHistoryResultType AddOperation(long id, OperationType operationType, long value, long balance);
}