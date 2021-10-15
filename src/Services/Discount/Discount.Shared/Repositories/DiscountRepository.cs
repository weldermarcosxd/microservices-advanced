using Dapper;
using Discount.Shared.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Discount.Shared.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> CreateDiscountASync(Coupon coupon, CancellationToken cancellationToken)
        {
            var affected = await _connection.ExecuteAsync(
                "INSERT INTO COUPON (PRODUCTNAME, DESCRIPTION, AMMOUNT) VALUES (@PRODUCTNAME, @DESCRIPTION, @AMMOUNT)",
                new
                {
                    PRODUCTNAME = coupon.ProductName,
                    DESCRIPTION = coupon.Description,
                    AMMOUNT = coupon.Ammount
                });

            return affected > decimal.Zero;
        }

        public async Task<bool> DeleteDiscountAsync(string productName, CancellationToken cancellationToken)
        {
            var affected = await _connection.ExecuteAsync("DELETE FROM COUPON WHERE PRODUCTNAME = @PRODUCTNAME", new { PRODUCTNAME = productName });
            return affected > decimal.Zero;
        }

        public async Task<Coupon> GetDiscountAsync(string productName, CancellationToken cancellationToken)
        {
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM COUPON WHERE PRODUCTNAME = @PRODUCTNAME", new { PRODUCTNAME = productName });
            return coupon ?? new Coupon { ProductName = "No Discount", Ammount = 0, Description = "No Discount Description" };
        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon, CancellationToken cancellationToken)
        {
            var affected = await _connection.ExecuteAsync(
                "UPDATE COUPON SET PRODUCTNAME = @PRODUCTNAME, DESCRIPTION = @DESCRIPTION, AMMOUNT = @AMMOUNT WHERE ID = @ID",
                new
                {
                    PRODUCTNAME = coupon.ProductName,
                    DESCRIPTION = coupon.Description,
                    AMMOUNT = coupon.Ammount,
                    ID = coupon.Id
                });

            return affected > decimal.Zero;
        }
    }
}