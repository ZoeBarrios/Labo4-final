using EcommerceAPI.Models.User;
using EcommerceAPI.Models.UserFavorite;
using EcommerceAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace EcommerceAPI.Repositories
{
    public interface IUserFavoriteRepository : IRepository<UserFavorite>
    {

    }
    public class UserFavoriteRepository : Repository<UserFavorite>, IUserFavoriteRepository
    {
        private readonly ApplicationDbContext _db;
        public UserFavoriteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public new async Task<UserFavorite> GetOne(Expression<Func<UserFavorite, bool>>? filter = null)
        {
            IQueryable<UserFavorite> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }

            return await query.FirstOrDefaultAsync();
        }

       
    }

}
