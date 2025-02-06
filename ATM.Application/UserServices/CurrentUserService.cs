using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.UserValueObject;

namespace ATM.Application.UserServices;

public class CurrentUserService : ICurrentUserService
{
    public User? User { get; set; }
}