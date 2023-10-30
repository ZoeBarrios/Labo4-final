using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.User.Dto
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string PhoneNumber { get; set; } = null!;


        [Required]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
    }
}
