using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Comment.Dto
{
    public class UpdateCommentDto
    {
      

        public string? Text { get; set; } = null!;

      
        public int? Rating { get; set; }
    }
}
