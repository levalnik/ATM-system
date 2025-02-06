namespace ATM.Application.Contracts.AdminServices;

public interface IAdminService
{
    ChangeAdminPasswordResultType ChangeAdminPassword(long oldPassword, long newPassword);

    AddNewUserResultType AddNewUser(long pin);
}