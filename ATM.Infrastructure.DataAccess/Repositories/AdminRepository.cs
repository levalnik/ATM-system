using ATM.Application.Abstractions.Repositories;
using ATM.Application.Models.AdminEntity;
using ATM.Application.Models.UserValueObject;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace ATM.Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Admin?> CheckPassword(long password)
    {
        const string sql = """
                           select *
                           from Admins
                           where admin_password = :password;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("password", password);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);

        if ((await reader.ReadAsync().ConfigureAwait(true)) is false)
            return null;

        return new Admin(
            Id: reader.GetInt64(0),
            Password: reader.GetInt64(1));
    }

    public async Task ChangeAdminPassword(long oldPassword, long newPassword)
    {
        const string sql = """
                           UPDATE Admins
                           SET admin_password = :newPassword
                           WHERE admin_password = :oldPassword;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("oldPassword", oldPassword);
        command.AddParameter("newPassword", newPassword);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);
    }

    public async Task<User?> AddUser(long pin)
    {
        const string sql = """
                           INSERT INTO users (user_pin, money)
                           VALUES (:pin, 0)
                           RETURNING *;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("pin", pin);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);
        if (await reader.ReadAsync().ConfigureAwait(true) is false)
            return null;

        return new User(
            Id: reader.GetInt64(0),
            Pin: reader.GetInt64(1),
            Money: reader.GetInt64(0));
    }
}