using Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.OrderHistoryDto
{
    public class GetOrderHistoryDto
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public Guid OrderId { get; set; }
        public string? Remarks { get; set; }
        public DateTime StatusUpdateDate { get; set; }
        public  GetOrderStatusShortDto OrderStatus { get; set; }
    }
}
