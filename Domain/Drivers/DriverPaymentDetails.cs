using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Drivers
{
    public class DriverPaymentDetails : BaseEntity
    {
        public Guid DriverPaymentHeadId { get; set; }
        public Guid OrderId { get; set; }
        public float OrderAmount { get; set; }
        public float CODAmount { get; set; }
        public float Total { get; set; }
        public virtual DriverPaymentHead DriverPaymentHead { get; set; }

    }
}
