using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models.UserFavorite
{
    public class UserFavorite
    {
        
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;

        public int PublicationId { get; set; }

        [ForeignKey("PublicationId")]
        public Publication.Publication Publication { get; set; } = null!;


        
    }
}
