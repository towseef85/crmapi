using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.OrderHistoryDto;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Dtos.VendorDto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.OrderDto
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public Guid VendorId { get; set; }
        public Guid DriverId { get; set; }
        public Guid PriceId { get; set; }
        public Guid StatusId { get; set; }
        public string? PickupLocation { get; set; }
        public int DeliveryType { get; set; }
        public float? CODCharges { get; set; }
        public string? Remarks { get; set; }
        public float? ExtraCharges { get; set; }
        public DateTime DeliveryDate { get; set; }
        public GetDriverDto Driver { get; set; }
        public GetVendorShortDto Vendor { get; set; }
        public ICollection<GetOrderHistoryDto> OrderHistory { get; set; }
        public GetPriceDto Prices { get; set; }
        public ICollection<GetOrderStatusShortDto> Status { get; set; }
        public float? OrderAmount { get; set; }
        public bool IsPaid { get; set; } = false;
    }

}
