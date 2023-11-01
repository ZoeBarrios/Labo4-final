using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Comment.Dto
{
    public class CreateCommentDto
    {

        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public int Rating { get; set; }


        [Required]
        public int UserId { get; set; }

     
        [Required]
        public int PublicationId { get; set; }

    
    }
}
