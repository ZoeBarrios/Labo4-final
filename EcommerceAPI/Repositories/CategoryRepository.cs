using EcommerceAPI.Models.Category;
using EcommerceAPI.Services;

namespace EcommerceAPI.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<Category> Update(Category entity);
    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async  Task<Category> Update(Category entity)
        {
             _db.Categories.Update(entity);
            await Save();
            return entity;
        }
    }
}
