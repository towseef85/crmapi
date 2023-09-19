using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class OrderStatus : BaseEntity
    {
        public string EngName { get; set; }
        public string ArbName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
