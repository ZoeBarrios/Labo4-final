using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models.Post.Dto
{
    public class CreatePostDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int UserId { get; set; }
    }
}
