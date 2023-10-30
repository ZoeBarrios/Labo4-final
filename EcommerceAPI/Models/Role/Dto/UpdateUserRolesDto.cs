using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models.Role.Dto
{
    public class UpdateUserRolesDto
    {
        [Required]
        public List<int> RoleIds { get; set; } = null!;
    }
}
