using ATM.Application.Abstractions.Repositories;
using ATM.Application.AdminServices;
using ATM.Application.Contracts.AccountMode;
using ATM.Application.Models.AccountTypes;
using ATM.Application.Models.AdminEntity;
using ATM.Application.Models.UserValueObject;
using ATM.Application.UserServices;

namespace ATM.Application.AccountModeServices;

public class AccountModeService : IAccountModeService
{
    private readonly CurrentAccountModeService _currentAccountModeService;
    private readonly IUserRepository _userRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly CurrentUserService _currentUserService;
    private readonly CurrentAdminService _currentAdminService;

    public AccountModeService(
        CurrentAccountModeService currentAccountModeService,
        IUserRepository userRepository,
        IAdminRepository adminRepository,
        CurrentUserService currentUserService,
        CurrentAdminService currentAdminService)
    {
        _currentAccountModeService = currentAccountModeService;
        _userRepository = userRepository;
        _adminRepository = adminRepository;
        _currentUserService = currentUserService;
        _currentAdminService = currentAdminService;
    }

    public LoginResultType LoginAdmin(long password)
    {
        Task<Admin?> admin = _adminRepository.CheckPassword(password);
        if (admin.Result is not null)
        {
            if (admin.Result.Password != password)
            {
                return new LoginResultType.InvalidPinFailure();
            }

            _currentAdminService.CurrentAdmin = admin.Result;
            _currentAccountModeService.CurrentUserMode = AccountType.Admin;
            return new LoginResultType.Success();
        }

        return new LoginResultType.AccountNotFound();
    }

    public LoginResultType LoginUser(long id, long password)
    {
        Task<User?> user = _userRepository.FindAccountById(id);
        if (user.Result is not null)
        {
            if (user.Result.Pin != password)
            {
                return new LoginResultType.InvalidPinFailure();
            }

            _currentUserService.User = user.Result;
            _currentAccountModeService.CurrentUserMode = AccountType.User;
            return new LoginResultType.Success();
        }

        return new LoginResultType.AccountNotFound();
    }
}