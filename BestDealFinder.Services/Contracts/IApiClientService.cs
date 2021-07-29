using System;
using System.Threading.Tasks;

namespace BestDealFinder.Services.Contracts
{
    public interface IApiClientService
    {
        Task MakeRequest(Models.ShippingProviders shippingProvider, object data, Action<string> onSuccess);
    }
}