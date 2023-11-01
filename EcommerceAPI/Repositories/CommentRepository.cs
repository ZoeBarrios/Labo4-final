using EcommerceAPI.Models.Comment;
using EcommerceAPI.Services;

namespace EcommerceAPI.Repositories
{
    public interface ICommentRepository:IRepository<Comment>
    {
        Task<Comment> Update(Comment entity);
    }
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db) 
        { 
        
            _db=db;
        }

        public async Task<Comment> Update(Comment entity)
        {
            _db.Comments.Update(entity);
            await Save();
            return entity;
        }
    }
}
