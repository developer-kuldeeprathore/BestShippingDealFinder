namespace BestDealFinder.Models
{
    public class ShippingCost
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public AddressModel ContactAddress { get; set; }

        public AddressModel WarehouseAddress { get; set; }

        public Dimension[] Dimensions { get; set; }
    }
}