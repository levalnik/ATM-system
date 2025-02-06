using ATM.Application.AccountModeServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.AccountTypes;
using System.Diagnostics.CodeAnalysis;

namespace ATM.Presentation.Console.Scenarios.UserScenarios.UserOperationScenarios.BalanceScenario;

public class UserBalanceScenarioProvider : IScenarioProvider
{
   private readonly CurrentAccountModeService _currentAccountModeService;
   private readonly IUserService _userService;

   public UserBalanceScenarioProvider(CurrentAccountModeService currentAccountModeService, IUserService userService)
   {
       _userService = userService;
       _currentAccountModeService = currentAccountModeService;
   }

   public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
   {
       if (_currentAccountModeService.CurrentUserMode == AccountType.User)
       {
           scenario = new UserBalanceScenario(_userService);
           return true;
       }

       scenario = null;
       return false;
   }
}