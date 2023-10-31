using EcommerceAPI.Models.Publication;
using EcommerceAPI.Services;

namespace EcommerceAPI.Repositories
{
    public interface IPublicationRepository: IRepository<Publication>
    {
        Task<Publication> Update(Publication entity);
    }
    public class PublicationRepository : Repository<Publication>, IPublicationRepository
    {
        private readonly ApplicationDbContext _db;

        public PublicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Publication> Update(Publication entity)
        {
            _db.Publications.Update(entity);
            await Save();
            return entity;
        }

    }
}
