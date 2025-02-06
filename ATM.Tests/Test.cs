using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.MoneyValueObject;
using ATM.Application.Models.UserValueObject;
using ATM.Application.UserServices;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace ATM.Tests;

public class Test
{
    [Fact]
    public void TopUpUserTest()
    {
        // Arrange
        var user = new User(1, 2222, 1000);
        CurrentUserService currentUserService = Substitute.For<CurrentUserService>();
        currentUserService.User = user;

        IUserRepository userRepository = Substitute.For<IUserRepository>();
        userRepository.TopUpAccount(user.Id, Arg.Any<Money>())
            .Returns(Task.FromResult(new Money(1300)));

        IOperationService operationService = Substitute.For<IOperationService>();

        var userService = new UserService(userRepository, currentUserService, operationService);

        // Act
        OperationResultType result = userService.TopUpAccount(300);
        var success = result as OperationResultType.Success;

        // Assert
        if (success != null) Assert.Equal(1300, success.Money.Value);
    }

    [Fact]
    public void WithdrawUserTest()
    {
        // Arrange
        var user = new User(1, 2222, 1000);
        CurrentUserService currentUserService = Substitute.For<CurrentUserService>();
        currentUserService.User = user;

        IUserRepository userRepository = Substitute.For<IUserRepository>();
        userRepository.TopUpAccount(user.Id, Arg.Any<Money>())
            .Returns(Task.FromResult(new Money(1300)));

        IOperationService operationService = Substitute.For<IOperationService>();

        var userService = new UserService(userRepository, currentUserService, operationService);

        // Act
        OperationResultType result = userService.WithdrawMoney(300);
        var success = result as OperationResultType.Success;

        // Assert
        if (success != null) Assert.Equal(700, success.Money.Value);
    }

    [Fact]
    public void NotEnoughMoneyTest()
    {
        {
            // Arrange
            var user = new User(1, 2222, 1000);
            CurrentUserService currentUserService = Substitute.For<CurrentUserService>();
            currentUserService.User = user;

            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.TopUpAccount(user.Id, Arg.Any<Money>())
                .Returns(Task.FromResult(new Money(1300)));

            IOperationService operationService = Substitute.For<IOperationService>();

            var userService = new UserService(userRepository, currentUserService, operationService);

            // Act
            OperationResultType result = userService.WithdrawMoney(1300);
            var failure = result as OperationResultType.Failure;

            // Assert
            if (failure != null) Assert.IsType<OperationResultType.Failure>(failure);
        }
    }
}