using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Category.Dto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
