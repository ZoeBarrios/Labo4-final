using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Publication.Dto
{
    public class UpdatePublicationDto
    {


        [MaxLength(128)]
        public string? Name { get; set; } 

        [MaxLength(512)]
        public string? Description { get; set; } 

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

       
    }
}
