
using Infrastructure.Dtos.PriceDto;

namespace Infrastructure.Dtos.VendorDto
{
    public class GetVendorDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string OwnerName { get; set; }
        public string MobileNo { get; set; }
        public string? OfficeNumber { get; set; }
        public string? CRNumber { get; set; }
        public string? VATNumber { get; set; }
        public string EmailId { get; set; }
        public string? LocationUrl { get; set; }
        public string LeadSource { get; set; }
        public string PickupAddress { get; set; }
        public ICollection<GetPriceDto> VendorPrices { get; set; }

    }
}
