using EcommerceAPI.Models.Publication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;
        [EmailAddress]
        [MaxLength(128)]
        public string? Email { get; set; }
        [Required]
        [StringLength(512,MinimumLength =6)]
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(128)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string PhoneNumber { get; set; } = null!;

        public List<Role.Role> Roles { get; set; } = null!;

        public List<Publication.Publication>? Publications { get; set; }

        public List<UserFavorite.UserFavorite>? UserFavorites { get; set; }  
    }
}
