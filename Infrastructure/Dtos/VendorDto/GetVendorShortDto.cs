using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.VendorDto
{
    public class GetVendorShortDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string OwnerName { get; set; }
        public string MobileNo { get; set; }
        public string? LocationUrl { get; set; }
        public string PickupAddress { get; set; }
    }
}
