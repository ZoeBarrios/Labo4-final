using EcommerceAPI.Models.Publication;
using EcommerceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories
{
    public interface IPublicationRepository: IRepository<Publication>
    {
        Task<Publication> Update(Publication entity);
        Task<List<Publication>> GetPage(int page,int pageSize);
    }
    public class PublicationRepository : Repository<Publication>, IPublicationRepository
    {
        private readonly ApplicationDbContext _db;

        public PublicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Publication>> GetPage(int page,int pageSize)
        {
            var skipAmount = (page - 1) * pageSize;
            var publications = await _db.Publications
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();
            return publications;
        }

        public async Task<Publication> Update(Publication entity)
        {
            _db.Publications.Update(entity);
            await Save();
            return entity;
        }



    }
}
