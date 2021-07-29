using BestDealFinder.Models;
using System.Threading.Tasks;

namespace BestDealFinder.Services.Contracts
{
    public interface IShippingService
    {
        Task<ShippingCostResponse[]> FetchShippingCost(ShippingRequestModel requestData);

        Task<ShippingCostResponse> FetchBestDeal(ShippingRequestModel requestData);
    }
}