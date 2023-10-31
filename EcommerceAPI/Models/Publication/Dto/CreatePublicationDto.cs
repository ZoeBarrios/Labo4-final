using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Publication.Dto
{
    public class CreatePublicationDto
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

        [Required]
        public int UserId { get; set; }

 
        [Required]
        public int CategoryId { get; set; }

    }
}
