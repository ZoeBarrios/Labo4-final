namespace EcommerceAPI.Models.Purchase.Dto
{
    public class PurchasesDto
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<Publication.Publication> Publications = null!;
    }
}
