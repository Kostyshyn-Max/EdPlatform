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
    public class CaseRepository : ICaseRepository
    {
        private readonly ApplicationDbContext _context;

        public CaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Case entity)
        {
            await _context.Cases.AddAsync(entity);
        }

        public async Task<Case?> Get(int id)
        {
            return await _context.Cases.FindAsync(id);
        }

        public async Task<IEnumerable<Case>> GetAll()
        {
            return await _context.Cases.ToListAsync();
        }

        public async Task<IEnumerable<Case>> Find(Expression<Func<Case, bool>> expression)
        {
            return await _context.Cases.Where(expression).ToListAsync();
        }

        public void Update(Case entity)
        {
            _context.Cases.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.Cases.Find(id);
            if (entity != null)
                _context.Cases.Remove(entity);
        }
    }
}
