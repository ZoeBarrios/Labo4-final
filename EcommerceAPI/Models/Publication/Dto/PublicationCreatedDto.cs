using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Publication.Dto
{
    public class PublicationCreatedDto
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(512)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public int? Stock { get; set; } = 1;

        public string? ImageUrl { get; set; }

        [Required]
        public int UserId { get; set; }


        [Required]
        public int CategoryId { get; set; }
    }
}
