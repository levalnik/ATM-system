using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.OperationServices;
using ATM.Application.Models.OperationEntity;
using ATM.Application.Models.Operations;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace ATM.Infrastructure.Extensions.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public OperationRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<IEnumerable<Operation>> GetOperationHistory(long id)
    {
        const string sql = """
                           select *
                           from operations
                           where user_id = :id
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(true);
        var result = new List<Operation>();

        while (await reader.ReadAsync().ConfigureAwait(true))
        {
            result.Add(new Operation(
                Id: reader.GetInt64(0),
                OperationType: await reader.GetFieldValueAsync<OperationType>(1).ConfigureAwait(true),
                Value: reader.GetInt64(2),
                Balance: reader.GetInt64(3)));
        }

        return result;
    }

    public async Task<CreateOperationHistoryResultType> AddOperation(
        long id,
        OperationType operationType,
        long value,
        long balance)
    {
        const string sql = """
                           INSERT INTO operations (user_id, operation, value, balance)
                           VALUES (:id, :operationType, :value, :balance)
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(true);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("operationType", operationType);
        command.AddParameter("value", value);
        command.AddParameter("balance", balance);
        int row = await command.ExecuteNonQueryAsync(default).ConfigureAwait(true);
        if (row == 0)
        {
            return new CreateOperationHistoryResultType.Failure();
        }

        return new CreateOperationHistoryResultType.Success();
    }
}