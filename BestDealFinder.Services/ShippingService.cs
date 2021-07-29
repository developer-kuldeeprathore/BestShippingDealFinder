using BestDealFinder.Models;
using BestDealFinder.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestDealFinder.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IEnumerable<IShippingProvider> _shippingProviders;

        public ShippingService(IEnumerable<IShippingProvider> shippingProvider)
        {
            _shippingProviders = shippingProvider;
        }

        public async Task<ShippingCostResponse[]> FetchShippingCost(ShippingRequestModel requestData)
        {
            if (requestData == null)
            {
                throw new ArgumentNullException(nameof(requestData));
            }

            var apiRequest = _shippingProviders.Select(provider => provider.FetchShippingCost(requestData));
            var shippingCostApiResponse = await Task.WhenAll(apiRequest);

            return shippingCostApiResponse?.Where(x => x.IsSuccess).ToArray();
        }

        public async Task<ShippingCostResponse> FetchBestDeal(ShippingRequestModel requestData)
        {
            var shippingCostResponses = await FetchShippingCost(requestData);
            var shippingCosts = shippingCostResponses.Select(s => new ShippingCostResponse
            {
                IsSuccess = true,
                ProviderName = s.ProviderName,
                Amount = s.ShippingCosts.Min(p => p.Amount)
            }).ToList();

            if (shippingCosts == null || shippingCosts.Count == 0)
                return null;

            return shippingCosts.OrderBy(x => x.Amount).FirstOrDefault();
        }

    }
}