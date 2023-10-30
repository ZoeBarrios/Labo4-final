using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApi.Models.Role
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; } = null!;
    }

    public class RoleUsers
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
