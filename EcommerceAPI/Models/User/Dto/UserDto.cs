

namespace EcommerceAPI.Models.User.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        
    }
}
