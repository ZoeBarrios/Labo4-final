using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models.Post.Dto
{
    public class PostsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
