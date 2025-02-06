using ATM.Application.Models.Operations;
using Itmo.Dev.Platform.Postgres.Plugins;
using Npgsql;

namespace ATM.Infrastructure.Extensions.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<OperationType>();
    }
}