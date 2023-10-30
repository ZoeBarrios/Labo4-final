using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using UsersApi.Models.User;
using UsersApi.Services;

namespace UsersApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Update(User entity);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await Save();
            return entity;
        }

        public new async Task<User> GetOne(Expression<Func<User, bool>>? filter = null)
        {
            IQueryable<User> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).Include(u => u.Roles);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}

