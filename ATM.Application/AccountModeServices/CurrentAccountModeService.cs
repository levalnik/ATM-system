using ATM.Application.Contracts.AccountMode;
using ATM.Application.Models.AccountTypes;

namespace ATM.Application.AccountModeServices;

public class CurrentAccountModeService : ICurrentAccountModeService
{
    public AccountType? CurrentUserMode { get; set; }
}