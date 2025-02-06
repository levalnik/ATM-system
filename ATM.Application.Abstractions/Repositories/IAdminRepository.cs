using ATM.Application.Models.AdminEntity;
using ATM.Application.Models.UserValueObject;

namespace ATM.Application.Abstractions.Repositories;

public interface IAdminRepository
{
    Task<Admin?> CheckPassword(long password);

    Task ChangeAdminPassword(long oldPassword, long newPassword);

    Task<User?> AddUser(long pin);
}