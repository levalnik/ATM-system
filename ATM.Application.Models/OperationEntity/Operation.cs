using ATM.Application.Models.Operations;

namespace ATM.Application.Models.OperationEntity;

public record Operation(long Id, OperationType OperationType, long Value, long Balance);