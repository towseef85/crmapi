using Domain.Common;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class OrderRequest : BaseEntity
    {
        public Guid VendorId { get; set; }
        public string? PickupLocation { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public float? CODCharges { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Remarks { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNumber { get; set; }
        public float? OrderAmount { get; set; }
        public virtual Vendor Vendor { get; set; }
        public bool OrderDone { get; set; } = false;
        public string? OrderNumber { get; set; }
    }
}
