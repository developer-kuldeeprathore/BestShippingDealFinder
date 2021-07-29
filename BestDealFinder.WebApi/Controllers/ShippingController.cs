using BestDealFinder.Models;
using BestDealFinder.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestDealFinder.WebApi.Controllers
{
    [EnableCors("sample-api")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private Random random = new Random();
        private readonly IShippingService _shippingDealFinder;

        public ShippingController(IShippingService shippingDealFinder)
        {
            _shippingDealFinder = shippingDealFinder;
        }

        /// <summary>
        /// Gets the shipping cost from providers with provider name and costing details.
        /// </summary>
        /// <response code="200"> Result: get success with list of different providers costing data.</response>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<ShippingCostResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShippingCost()
        {
            var shippingCostResponses = await _shippingDealFinder.FetchShippingCost(new ShippingRequestModel());

            return Ok(shippingCostResponses);
        }

        /// <summary>
        /// Find best deal from all shipping provider cost
        /// </summary>
        /// <returns>minimum amount deal.</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ShippingCostResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBestDeal()
        {
            // requested model used to filter data or pass specific parameter.
            var requestData = new ShippingRequestModel();
            var bestDeal = await _shippingDealFinder.FetchBestDeal(requestData);

            return Ok($"Best shipping deal provided by {bestDeal.ProviderName} : ${bestDeal.Amount}");
        }

        /// <summary>
        /// Gets mock api shipping data as reference in different format like : JSON/ XML.
        /// </summary>
        /// <param name="provider">spicify provider name in routes</param>
        /// <param name="pagesize">specify page size for no of records. default size = 10</param>
        /// <response code="200"> Result: success with specific provider costing data.</response>
        [HttpGet("{provider:shippingProviders}/[action].{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<ShippingCostResponse>), StatusCodes.Status200OK)]
        public ShippingCostResponse GetCost(ShippingProviders provider, int pagesize = 10)
        {
            var sampleData = new ShippingCostResponse
            {
                ProviderName = provider.ToString(),
                IsSuccess = true,
                ShippingCosts = new List<ShippingCost>()
            };

            double min = 0.50;
            double max = 99.99;

            for (int index = 0; index < pagesize; index++)
            {
                sampleData.ShippingCosts.Add(new ShippingCost
                {
                    Id = index + 1,
                    Amount = decimal.Round(GetPrice(min, max), 2, MidpointRounding.AwayFromZero)
                });
            }

            return sampleData;
        }

        /// <summary>
        /// Generate random price for mock data
        /// </summary>
        /// <returns>price</returns>
        private decimal GetPrice(double minimum, double maximum)
        {
            return (decimal)(random.NextDouble() * (maximum - minimum) + minimum);
        }
    }
}