using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.Role;

namespace EcommerceAPI.Services
{
    public class AuthService
    {
        private string secretKey;
        private readonly UserService _userService;
        public AuthService(IConfiguration config, UserService userService)
        {
            secretKey = config.GetSection("jwtSettings:secretKey").ToString() ?? null!;
            _userService = userService;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("id", user.Id.ToString()));
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return token;
        }



        public async Task<bool> IsUserAuthorized(int idUser, int idAction)
        {
            try
            {
                var user = await _userService.GetById(idUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user data.");
            }

            var roles = await _userService.GetRolesOfUserById(idUser);
            bool isAdmin = false;

            foreach (Role role in roles)
            {
                if (role.Name == "Admin")
                {
                    isAdmin = true;
                    break;
                }
            }

            if (!isAdmin)
            {
                if (idAction != idUser)
                {
                    throw new Exception("Error fetching user data.");
                }
            }

            return true;

        }

    }
     
}
