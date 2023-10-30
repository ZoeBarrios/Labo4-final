using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models.Post.Dto
{
    public class UpdatePostDto
    {
        [MaxLength(50)]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
