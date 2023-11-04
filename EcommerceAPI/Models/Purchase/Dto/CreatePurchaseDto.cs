using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Purchase.Dto
{
    public class CreatePurchaseDto
    {
       
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SellerId { get; set; }

        [Required]
        public List<int> PublicationsIds { get; set; } = null!;

       
     
    }

   
}
