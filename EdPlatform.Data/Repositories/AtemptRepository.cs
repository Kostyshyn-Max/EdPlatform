using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class AttemptRepository : IAttemptRepository
    {
        private readonly ApplicationDbContext _context;

        public AttemptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Attempt entity)
        {
            await _context.Attempts.AddAsync(entity);
        }
        
        public async Task<Attempt?> Get(int id)
        {
            return await _context.Attempts.FindAsync(id);
        }

        public async Task<IEnumerable<Attempt>> GetAll()
        {
            return await _context.Attempts.ToListAsync();
        }

        public async Task<IEnumerable<Attempt>> Find(Expression<Func<Attempt, bool>> expression)
        {
            return await _context.Attempts.Where(expression).ToListAsync();
        }

        public void Update(Attempt entity)
        {
            _context.Attempts.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.Attempts.Find(id);
            if (entity != null)
                _context.Attempts.Remove(entity);
        }
    }
}
