using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Comment entity)
        {
            _context.Comments.Add(entity);
        }

        public Comment Get(int id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments;
        }

        public void Update(Comment entity)
        {
            _context.Comments.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Comments.Find(id);
            if (entity != null)
                _context.Comments.Remove(entity);
        }
    }
}
