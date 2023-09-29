using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.DriverpaymentDto
{
    public class PostDriverPaymentDetailsDto
    {
        public Guid DriverPaymentHeadId { get; set; }
        public Guid OrderId { get; set; }
        public float OrderAmount { get; set; }
        public float CODAmount { get; set; }
        public float Total { get; set; }
    }
}
