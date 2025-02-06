using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.AdminServices;
using ATM.Application.Models.AdminEntity;
using ATM.Application.Models.UserValueObject;

namespace ATM.Application.AdminServices;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public ChangeAdminPasswordResultType ChangeAdminPassword(long oldPassword, long newPassword)
    {
        Task<Admin?> admin = _adminRepository.CheckPassword(oldPassword);
        if (admin.Result == null)
        {
            return new ChangeAdminPasswordResultType.InvalidPassword();
        }

        _adminRepository.ChangeAdminPassword(oldPassword, newPassword);
        return new ChangeAdminPasswordResultType.Success();
    }

    public AddNewUserResultType AddNewUser(long pin)
    {
        Task<User?> user = _adminRepository.AddUser(pin);
        if (user.Result == null)
        {
            return new AddNewUserResultType.Failure();
        }

        return new AddNewUserResultType.Success(user.Result.Id, user.Result.Pin);
    }
}