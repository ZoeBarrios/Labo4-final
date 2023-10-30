using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Role.Dto
{
    public class UpdateUserRolesDto
    {
        [Required]
        public List<int> RoleIds { get; set; } = null!;
    }
}
