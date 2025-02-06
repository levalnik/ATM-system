using ATM.Application.Contracts.AdminServices;
using ATM.Application.Models.AdminEntity;

namespace ATM.Application.AdminServices;

public class CurrentAdminService : ICurrentAdminService
{
    public Admin? CurrentAdmin { get; set; }
}