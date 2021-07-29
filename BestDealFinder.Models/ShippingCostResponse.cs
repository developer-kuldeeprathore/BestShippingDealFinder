using System.Collections.Generic;

namespace BestDealFinder.Models
{
    public class ShippingCostResponse
    {
        public bool IsSuccess { get; set; }
        public string ProviderName { get; set; }

        public decimal Amount { get; set; }
        public List<ShippingCost> ShippingCosts { get; set; }
    }
}