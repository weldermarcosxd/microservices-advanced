using Discount.Grpc.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Discount.Grpc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection, NpgsqlConnection>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
        }
    }
}