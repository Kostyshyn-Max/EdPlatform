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
    public class IOCaseRepository : IIOCaseRepository
    {
        private readonly ApplicationDbContext _context;
        public IOCaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(IOCase entity)
        {
            await _context.IOCases.AddAsync(entity);
        }

        public async Task<IOCase?> Get(int id)
        {
            return await _context.IOCases.FindAsync(id);
        }

        public async Task<IEnumerable<IOCase>> GetAll()
        {
            return await _context.IOCases.ToListAsync();
        }

        public async Task<IEnumerable<IOCase>> Find(Expression<Func<IOCase, bool>> expression)
        {
            return await _context.IOCases.Where(expression).ToListAsync();
        }

        public void Update(IOCase entity)
        {
            _context.IOCases.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.IOCases.Find(id);
            if (entity != null)
                _context.IOCases.Remove(entity);
        }
    }
}
