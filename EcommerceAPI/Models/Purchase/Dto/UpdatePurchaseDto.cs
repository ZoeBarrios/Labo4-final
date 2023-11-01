using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Purchase.Dto
{
    public class UpdatePurchaseDto
    {
        [Required]
        public List<int> PublicationsIds { get; set; } = null!;
    }
}
