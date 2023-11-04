

using EcommerceAPI.Models.Publication.Dto;

namespace EcommerceAPI.Models.Purchase.Dto
{
    public class PurchaseDto
    {

        public int PurchaseId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int SellerId { get; set; }

        public List<PublicationsDto> Publications { get; set; } = null!;

    }
}
