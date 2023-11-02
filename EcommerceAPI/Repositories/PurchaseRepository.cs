using EcommerceAPI.Models.Purchase;
using EcommerceAPI.Models.User;
using EcommerceAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Purchase.Dto;

namespace EcommerceAPI.Repositories
{
    public interface IPurchaseRepository: IRepository<Purchase>
    {
        Task<Purchase> Update(Purchase entity);
        Task<List<Purchase>> GetAllPurchases(Expression<Func<Purchase, bool>>? filter = null);

        Task<Purchase> GetOnePurchase(Expression<Func<Purchase, bool>>? filter = null);


    }
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Purchase> Update(Purchase entity)
        {
            _db.Purchases.Update(entity);
            await Save();
            return entity;
        }

        public async Task<List<Purchase>> GetAllPurchases(Expression<Func<Purchase, bool>>? filter = null)
        {

            IQueryable<Purchase> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).Include(p => p.Publications);

            }

            return await query.ToListAsync();

           
        }

        public async Task<Purchase> GetOnePurchase(Expression<Func<Purchase, bool>>? filter = null)
        {

            IQueryable<Purchase> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).Include(p => p.Publications);

            }

            return await query.FirstOrDefaultAsync();

            

            
        }
    }
}
