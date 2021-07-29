using BestDealFinder.Models;
using System.Threading.Tasks;

namespace BestDealFinder.Services.Contracts
{
    public interface IShippingProvider
    {
        Task<ShippingCostResponse> FetchShippingCost(ShippingRequestModel shippingRequest);
    }
}