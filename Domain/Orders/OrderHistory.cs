using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class OrderHistory : BaseEntity
    {
        public Guid StatusId { get; set; }
        public Guid OrderId { get; set; }
        public string? Remarks { get; set; }
        public DateTime StatusUpdateDate { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Order Order { get; set; }


    }
}
