using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.UserFavorite.Dto
{
    public class CreateUserFavoriteDto
    {
        [Required]

        public int UserId { get; set; }


        [Required]

        public int PublicationId { get; set; }
    }
}
