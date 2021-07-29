namespace BestDealFinder.Models
{
    public class ShippingProviderApiDetail
    {
        public string ProviderName { get; set; }
        public ResponseType ResponseType { get; }
        public string ApiURL { get; set; }
        public ApiCredentials ApiCredentials { get; set; }
    }
}