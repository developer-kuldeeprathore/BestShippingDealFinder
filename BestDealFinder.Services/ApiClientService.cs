using BestDealFinder.Models;
using BestDealFinder.Services.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BestDealFinder.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly List<ShippingProviderApiDetail> _shippingProviders;

        public ApiClientService(IOptions<List<ShippingProviderApiDetail>> providersOptions)
        {
            _shippingProviders = providersOptions.Value;
            _httpClient = new HttpClient();
        }

        public async Task MakeRequest(Models.ShippingProviders shippingProvider, object data, Action<string> onSuccess)
        {
            try
            {
                var shippingProviderDetail = _shippingProviders.FirstOrDefault(p => p.ProviderName == shippingProvider.ToString());

                if (shippingProviderDetail == null)
                    throw new ArgumentNullException(nameof(shippingProviderDetail));

                if (string.IsNullOrEmpty(shippingProviderDetail.ApiURL))
                {
                    throw new Exception("API URL Could not be empty.");
                }

                var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8);

                var response = await _httpClient.GetAsync(shippingProviderDetail.ApiURL);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    onSuccess?.Invoke(responseContent);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}