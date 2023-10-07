using Infrastructure.Dtos.VendorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.OrderRequestDto
{
    public class GetOrderRequestDto
    {
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public string? PickupLocation { get; set; }
        public int DeliveryType { get; set; }
        public float? CODCharges { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Remarks { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNumber { get; set; }
        public float? OrderAmount { get; set; }
        public virtual GetVendorShortDto Vendor { get; set; }
        public bool OrderDone { get; set; }
    }
}
