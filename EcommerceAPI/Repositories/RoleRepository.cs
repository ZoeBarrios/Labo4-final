using EcommerceAPI.Models.Role;
using EcommerceAPI.Services;

namespace EcommerceAPI.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> Update(Role entity);
    }
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Role> Update(Role entity)
        {
            _db.Roles.Update(entity);
            await Save();
            return entity;
        }
    }
}
