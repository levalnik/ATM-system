using ATM.Application.Models.UserValueObject;

namespace ATM.Application.Contracts.UserServices;

public interface ICurrentUserService
{
    User? User { get; set; }
}