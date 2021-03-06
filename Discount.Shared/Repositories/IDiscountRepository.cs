using Discount.Shared.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Discount.Shared.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscountAsync(string productName, CancellationToken cancellationToken);

        Task<bool> CreateDiscountASync(Coupon coupon, CancellationToken cancellationToken);

        Task<bool> UpdateDiscountAsync(Coupon coupon, CancellationToken cancellationToken);

        Task<bool> DeleteDiscountAsync(string productName, CancellationToken cancellationToken);
    }
}