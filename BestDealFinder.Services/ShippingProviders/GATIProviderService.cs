using BestDealFinder.Models;
using BestDealFinder.Services.Contracts;
using System.Threading.Tasks;

namespace BestDealFinder.Services.ShippingProviders
{
    public class GATIProviderService : ResponseHandlerBase, IShippingProvider
    {
        private readonly IApiClientService _apiClientService;
        private ShippingCostResponse _shippingCostResponse;

        public GATIProviderService(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<ShippingCostResponse> FetchShippingCost(ShippingRequestModel shippingRequest)
        {
            _shippingCostResponse = new ShippingCostResponse();

            await _apiClientService.MakeRequest(Models.ShippingProviders.GATI, shippingRequest, xmlResponse =>
            {
                if (xmlResponse != string.Empty)
                {
                    _shippingCostResponse = ParseFrom<ShippingCostResponse>(ResponseType.Xml, xmlResponse);
                }
            });

            return _shippingCostResponse;
        }
    }
}