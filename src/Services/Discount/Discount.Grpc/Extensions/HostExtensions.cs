using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;

namespace Discount.Grpc.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvalilability = retry.Value;
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migration postgres database");
                using var connection = new NpgsqlConnection(configuration.GetValue<string>("databaseSettings:ConnectionString"));
                connection.Open();
                using var command = new NpgsqlCommand { Connection = connection };
                command.CommandText = "DROP TABLE IF EXISTS COUPON";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE COUPON(
                    ID SERIAL PRIMARY KEY,
                    PRODUCTNAME VARCHAR(24) NOT NULL,
                    DESCRIPTION TEXT,
                    AMMOUNT INT
                )";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO COUPON(PRODUCTNAME, DESCRIPTION, AMMOUNT) VALUES ('IPHONE X', 'IPHONE DISCOUNT', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO COUPON(PRODUCTNAME, DESCRIPTION, AMMOUNT) VALUES ('SAMSUNG 10', 'SAMSUNG DISCOUNT', 110);";
                command.ExecuteNonQuery();

                logger.LogInformation("Migrated postgres database");
            }
            catch (Exception)
            {
                logger.LogInformation("Am error occourrer while migration the postgres database");
                if (retryForAvalilability < 50)
                {
                    retryForAvalilability++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvalilability);
                }
            }

            return host;
        }
    }
}