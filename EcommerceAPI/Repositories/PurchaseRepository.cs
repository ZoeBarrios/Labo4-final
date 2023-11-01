using EcommerceAPI.Models.Purchase;
using EcommerceAPI.Models.User;
using EcommerceAPI.Services;

namespace EcommerceAPI.Repositories
{
    public interface IPurchaseRepository: IRepository<Purchase>
    {
        Task<Purchase> Update(Purchase entity);
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
    }
}
