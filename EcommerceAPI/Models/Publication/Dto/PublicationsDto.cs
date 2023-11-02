namespace EcommerceAPI.Models.Publication.Dto
{
    public class PublicationsDto
    {
        public int PublicationId { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

      
    }
}
