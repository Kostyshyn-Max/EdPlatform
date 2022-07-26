using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task Create(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
        }

        public async Task<Comment> Get(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
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
