using BestDealFinder.Models;
using BestDealFinder.Services.Contracts;
using System.Threading.Tasks;

namespace BestDealFinder.Services.ShippingProviders
{
    public class DHLProviderService : ResponseHandlerBase, IShippingProvider
    {
        private readonly IApiClientService _apiClientService;
        private ShippingCostResponse _shippingCostResponse;

        public DHLProviderService(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<ShippingCostResponse> FetchShippingCost(ShippingRequestModel shippingRequest)
        {
            _shippingCostResponse = new ShippingCostResponse();

            await _apiClientService.MakeRequest(Models.ShippingProviders.DHL, shippingRequest, xmlResponse =>
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