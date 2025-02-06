using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace ATMSystem.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
               create type operation_type as enum
               (
                   'top_up_account',
                   'withdraw_money'
               );
               create table users
               (
                   user_id bigint primary key generated always as identity,
                   user_pin bigint not null,
                   money bigint not null
               );
               create table admins
               (
                   admin_id bigint primary key generated always as identity,
                   admin_password  bigint not null
               );
               create table operations
               (
                   user_id bigint not null references users(user_id),
                   operation operation_type not null,
                   value bigint not null,
                   balance bigint not null
               );
               """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table users;
        drop table admins;
        drop table operations; 
        drop type operation_type;
        """;
}