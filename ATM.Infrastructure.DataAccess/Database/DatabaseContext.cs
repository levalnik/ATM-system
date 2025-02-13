using Microsoft.EntityFrameworkCore;

namespace ATM.Infrastructure.DataAccess.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }
}