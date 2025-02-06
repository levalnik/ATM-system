using ATM.Application.Models.AdminEntity;

namespace ATM.Application.Contracts.AdminServices;

public interface ICurrentAdminService
{
    Admin? CurrentAdmin { get; }
}