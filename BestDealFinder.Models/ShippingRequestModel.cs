using System.ComponentModel.DataAnnotations;

namespace BestDealFinder.Models
{
    public class ShippingRequestModel
    {
        [Required]
        public AddressModel ContactAddress { get; set; }

        [Required]
        public AddressModel WarehouseAddress { get; set; }

        public Dimension[] Dimensions { get; set; }
    }
}