﻿using Domain.Common;
using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class Order : BaseEntity
    {
        public Guid VendorId { get; set; }
        public string OrderNumber { get; set; }
        public Guid DriverId { get; set; }
        public Guid PriceId { get; set; }
        public string? PickupLocation { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public float? CODCharges { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? DeliveryTime { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Price Price { get; set; }
        public float? OrderAmount { get; set; }
        public ICollection<OrderHistory> OrderHistory { get; set; }
        public string? Remarks { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNumber { get; set; }
        public float? ExtraCharges { get; set; }
        public bool IsPaid { get; set; } = false;
        public Guid? OrderRequestId { get; set; }

    }

    public enum DeliveryType { COD, Online }


}
