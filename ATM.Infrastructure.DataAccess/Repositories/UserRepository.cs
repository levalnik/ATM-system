using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.UserServices;
using ATM.Application.Models.MoneyValueObject;
using ATM.Application.Models.UserValueObject;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace ATM.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<User?> FindAccountById(long id)
    {
        const string sql = """
                           select *
                           from Users
                           where user_id = :id;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);

        if ((await reader.ReadAsync().ConfigureAwait(true)) is false)
        {
            return null;
        }

        return
            new User(
            Id: reader.GetInt64(0),
            Pin: reader.GetInt64(1),
            Money: reader.GetInt64(2));
    }

    public async Task<Money?> Balance(long id)
    {
        const string sql = """
                           select money
                           from Users
                           where user_id = :id;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);

        return await reader.ReadAsync().ConfigureAwait(true) is false ? null : new Money(reader.GetInt64(0));
    }

    public async Task<OperationResultType> WithdrawMoney(long id, Money updateMoney)
    {
        const string sql = """
                           UPDATE users
                           SET money = money - :updateMoney.Value
                           WHERE user_id = :id
                           AND money >= :updateMoney.Value
                           RETURNING money;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("updateMoney.Value", updateMoney.Value);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);

        if (await reader.ReadAsync().ConfigureAwait(true) is false)
            return new OperationResultType.Failure();
        return new OperationResultType.Success(new Money(reader.GetInt64(0)));
    }

    public async Task<Money> TopUpAccount(long id, Money updateMoney)
    {
        const string sql = """
                           UPDATE Users
                           SET money = money + :updateMoney.Value
                           WHERE user_id = :id
                           RETURNING money;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("updateMoney.Value", updateMoney.Value);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);
        await reader.ReadAsync().ConfigureAwait(true);
        return new Money(reader.GetInt64(0));
    }
}