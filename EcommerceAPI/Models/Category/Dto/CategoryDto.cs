using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Category.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

 
        public string Name { get; set; } = null!;
    }
}
