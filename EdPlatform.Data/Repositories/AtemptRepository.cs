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
    public class AtemptRepository : IRepository<Atempt>
    {
        private readonly ApplicationDbContext _context;

        public AtemptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Atempt entity)
        {
            await _context.Atempts.AddAsync(entity);
        }
        
        public async Task<Atempt> Get(int id)
        {
            return await _context.Atempts.FindAsync(id);
        }

        public async Task<IEnumerable<Atempt>> GetAll()
        {
            return await _context.Atempts.ToListAsync();
        }

        public void Update(Atempt entity)
        {
            _context.Atempts.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Atempts.Find(id);
            if (entity != null)
                _context.Atempts.Remove(entity);
        }
    }
}
