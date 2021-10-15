using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public interface IDiscountGrpcService
    {
        Task<CouponModel> GetDiscountAsync(string productName);
    }
}