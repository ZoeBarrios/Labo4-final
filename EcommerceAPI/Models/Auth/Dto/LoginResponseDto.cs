using EcommerceAPI.Models.User.Dto;

namespace EcommerceAPI.Models.Auth.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public UserLoginResponseDto User { get; set; } = null!;
    }
}
