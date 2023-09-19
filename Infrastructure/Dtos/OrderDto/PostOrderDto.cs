using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.OrderDto
{
    public class PostOrderDto
    {
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public Guid DriverId { get; set; }
        public Guid PriceId { get; set; }
        public string? PickupLocation { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public float? CODCharges { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

}
