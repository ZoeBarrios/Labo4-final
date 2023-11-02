using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.User.Dto
{
    public class UpdateUserDto
    {
        [MaxLength(40)]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } 
        public string? UserName { get; set; }

    }
}
