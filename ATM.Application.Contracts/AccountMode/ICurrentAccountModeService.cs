using ATM.Application.Models.AccountTypes;

namespace ATM.Application.Contracts.AccountMode;

public interface ICurrentAccountModeService
{
    AccountType? CurrentUserMode { get; }
}